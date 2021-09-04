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
  [Route("{id}")]
  public class ProfileController : ControllerBase
  {
   private readonly ProfileService _profileService;

    public ProfileController(ProfileService profileService)
    {
      _profileService = profileService;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Profile>> Get(int id)
    {
        try
        {
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            //  Profile profile = _accountService.GetOrCreateProfile(id);
            //  return Ok(profile);
        }
        catch (Exception err)
        {
            
            return BadRequest(err.Message);
        }
    }
  }

}