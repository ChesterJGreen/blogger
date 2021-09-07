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
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly BlogsService _blogsService;
        private readonly CommentsService _commentsService;

        public AccountController(AccountService accountService, BlogsService blogsService, CommentsService commentsService)
        {
            _accountService = accountService;
             _blogsService = blogsService;
             _commentsService = commentsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("blogs")]
        [Authorize]
        public async Task<ActionResult<Blog>> Get(string id)
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

        [HttpGet("comments")]
        [Authorize]
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

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Account>> Edit([FromBody] Account editData, string id )
    {
        try
        {
             Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.Edit(editData, userInfo));
        }
        catch (Exception err)
        {
            
            return BadRequest(err.Message);
        }
    }
    }


}