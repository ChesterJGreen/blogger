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

    public BlogsController(BlogsService blogsService)
    {
      _blogsService = blogsService;
    }
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

  }
}