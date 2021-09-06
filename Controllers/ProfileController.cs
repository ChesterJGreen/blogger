using Microsoft.AspNetCore.Mvc;
using blogger.Services;
using System;
using System.Collections.Generic;
using blogger.Models;
using CodeWorks.Auth0Provider;
using System.Threading.Tasks;

namespace blogger.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProfileController : ControllerBase
  {
   private readonly AccountService _accountService;
   private readonly BlogsService _blogsService;
   private readonly CommentsService _commentsService;

    public ProfileController(AccountService accountService)
    {
      _accountService = accountService;
    }
    public ProfileController(BlogsService blogsService)
    {
        _blogsService = blogsService;
    }
    public ProfileController(CommentsService commentsService)
    {
        _commentsService = commentsService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Profile>> Get(string id)
    {
        try
        {
             Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
             Profile profile = _accountService.GetProfile(id);
             return Ok(profile);
             
        }
        catch (Exception err)
        {            
            return BadRequest(err.Message);
        }
    }
    [HttpGet("{id}/blogs")]
    // proof of routing
    public async Task<ActionResult<Blog>> GetBlogsByProfileId(string id)
    {
        try
        {
             Account UserInfo = await HttpContext.GetUserInfoAsync<Account>();
             if( UserInfo.Id != null)
             {
             List<Blog> blogs = _blogsService.GetAllByProfileId(id);
             return Ok(blogs);
             }
             else 
             {
                 throw new Exception("profile does not exist");
             }
             
        }
        catch (Exception err)
        {            
            return BadRequest(err.Message);
        }
    }
    [HttpGet("{id}/comments")]
    public async Task<ActionResult<Comment>> GetCommentsByProfileId(string id)
    {
        try
        {
             Account UserInfo = await HttpContext.GetUserInfoAsync<Account>();
             if( UserInfo.Id != null)
             {
             List<Comment> comments = _commentsService.GetAllByProfileId(id);
             return Ok(comments);
             }
             else 
             {
                 throw new Exception("profile does not exist");
             }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
  }

}