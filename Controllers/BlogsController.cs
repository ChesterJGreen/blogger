using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using blogger.Models;
using blogger.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blogger.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly BlogsService _blogsService;
        private readonly CommentsService _commentsService;

    public BlogsController(BlogsService blogsService)
    {
      _blogsService = blogsService;
    }
    // public BlogsController(CommentsService commentsService)
    // {
    //   _commentsService = commentsService;
    // }
    [HttpGet]
    public ActionResult<List<Blog>> Get()
    {
        try
        {
             List<Blog> blogs = _blogsService.Get();
             return Ok(blogs);
        }
        catch (Exception err)
        {
            
            return BadRequest(err.Message);
        }
    }
    [HttpGet("{id}")]
    public ActionResult<Blog> Get(int id)
    {
        try
        {
             Blog blog = _blogsService.Get(id);
             return Ok(blog);
        }
        catch (Exception err)
        {
            
            return BadRequest(err.Message);
        }
    }
    [HttpGet("{id}/comments")]
    public ActionResult<Comment> GetCommentByBlogId(int id)
    {
        try
        {
            Blog blog = _blogsService.Get(id);
            if( blog != null)
            {

             List<Comment> comments = _commentsService.GetCommentsByBlogId(id);
             return Ok(comments);
            }
            else
            {
                return BadRequest("That Blog does not exist");
            }
        }
        catch (Exception err)
        {
            
            return BadRequest(err.Message);
        }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Blog>> CreateAsync([FromBody] Blog newBlog)
    {
        try
        {
             Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
             newBlog.CreatorId = userInfo.Id;
             Blog blog = _blogsService.Create(newBlog);
             return Ok(blog);
        }
        catch (Exception err)
        {
            
            return BadRequest(err.Message);
        }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Blog>> Edit([FromBody] Blog updatedBlog, int id)
    {
        try
        {
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            Blog oldBlog = _blogsService.Get(id);

            if (oldBlog.CreatorId == userInfo.Id && updatedBlog.CreatorId == userInfo.Id && updatedBlog.Id == id)
            {
                Blog blog = _blogsService.Edit(oldBlog, updatedBlog);
                return Ok(blog);
            }
            else 
            {
                return BadRequest("You do not have permission to edit");
            }
             
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteAsync(int id)
    {
        try
        {
             Account UserInfo = await HttpContext.GetUserInfoAsync<Account>();
            _blogsService.Delete(id, UserInfo.Id);
            return Ok("Deleted ");
        }
        catch (Exception err)
        {
            
            return BadRequest(err.Message);
        }
    }

  }
}