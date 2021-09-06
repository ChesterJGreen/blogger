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
    public class CommentsController :ControllerBase
    {
    private readonly CommentsService _commentsService;

    public CommentsController(CommentsService commentsService)
    {
      _commentsService = commentsService;
    }
    [HttpGet]
    public ActionResult<List<CommentsService>> Get()
    {
        try
        {
            List<Comment> comments = _commentsService.Get();
            return Ok(comments);
        }
            catch (Exception err)
        {
            
            return BadRequest(err.Message);
        }
    }
    [HttpGet("{id}")]
    public ActionResult<Comment> Get(int id)
    {
        try
        {
             Comment comment = _commentsService.Get(id);
             return Ok(comment);
        }
        catch (Exception err)
        {
            
            return BadRequest(err.Message);
        }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Comment>> Create([FromBody] Comment newComment)
    {
        try
        {
             Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
             newComment.CreatorId = userInfo.Id;
             Comment comment = _commentsService.Create(newComment);
             return Ok(comment);
        }
        catch (Exception err)
        {
            
            return BadRequest(err.Message);
        }
    }
    [HttpPut("{id}")]
    [Authorize]

        public async Task<ActionResult<Comment>> Edit([FromBody] Comment updatedComment, int id)
    {
        try
        {
             Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            Comment oldComment = _commentsService.Get(id);
            if(oldComment.CreatorId == userInfo.Id && updatedComment.CreatorId == userInfo.Id && updatedComment.Id ==id)
            {
             Comment comment = _commentsService.Edit(oldComment, updatedComment);
             return Ok(comment);             

            }
        }
        catch (Exception err)
        {
            
            return BadRequest(err.Message);
        }
    }
    [HttpDelete]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
        try
        {
              Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
             _commentsService.Delete(id, userInfo.Id);
             return Ok("Deleted");
        }
        catch (Exception err)
        {
            
            return BadRequest(err.Message);
        }
    }

  }
}