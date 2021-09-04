using System;
using System.Collections.Generic;
using blogger.Models;
using blogger.Services;
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
  }
}