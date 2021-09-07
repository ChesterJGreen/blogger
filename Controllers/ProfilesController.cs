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
  [Route("/api/[controller]")]
  public class ProfilesController : ControllerBase
  {
   private readonly AccountService _accountService;
   private readonly BlogsService _blogsService;
   private readonly CommentsService _commentsService;

    public ProfilesController(AccountService accountService, BlogsService blogsService, CommentsService commentsService)
    {
      _accountService = accountService;
      _blogsService = blogsService;
      _commentsService = commentsService;
    }
 
    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
        try
        {
             Profile profile = _accountService.GetProfileById(id);
             return profile;
             
        }
        catch (Exception err)
        {            
            return BadRequest(err.Message);
        }
    }
    [HttpGet("{id}/blogs")]
    public ActionResult<List<Blog>> GetBlogsByProfileId(string id)
    {
        try
        {
            List<Blog> blogs = _blogsService.GetAllByProfileId(id);
            return blogs;             
        }
        catch (Exception err)
        {            
            return BadRequest(err.Message);
        }
    }
    [HttpGet("{id}/comments")]
    public ActionResult<Comment> GetCommentsByProfileId(string id)
    {
        try
        {
            List<Comment> comments = _commentsService.GetAllByProfileId(id);
            return Ok(comments);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
  }

}