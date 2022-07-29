using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterAPI.Authentication;
using TwitterAPI.DTOs.AccountDTOs;
using TwitterAPI.DTOs.Notifications;
using TwitterAPI.DTOs.TweetDTOs;
using TwitterAPI.Services.AuthenticationServices.Interface;

namespace TwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountServices _accountServices;

        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpPost("FollowAccount")]
        public async Task<ActionResult> FollowAccount(string account)
        {
            var result = await _accountServices.FollowAccount(User.Identity.Name, account);
            if (result == null)
            {
                return BadRequest("The account you tried to follow does not exist");
            }
            return Ok(result);
        }



        [Authorize(Roles = UserRoles.User)]
        [HttpGet("ViewAccount")]
        public async Task<ActionResult<ViewAccountDTO>> ViewAccount(string username)
        {
            var result = await _accountServices.GetAccount(username);
            if(result == null)
            {
                return (BadRequest("The account does not exist"));
            }
            return Ok(result);
        }


        [Authorize(Roles = UserRoles.User)]
        [HttpGet("ViewFollowers")]
        public async Task<ActionResult<IEnumerable<ViewAccountDTO>>> ViewFollowers(string username)
        {
            var result = await _accountServices.GetFollowers(username);
            return Ok(result);
        }


        [Authorize(Roles = UserRoles.User)]
        [HttpGet("SearchAccounts")]
        public async Task<ActionResult<IEnumerable<ViewAccountDTO>>> SearchAccounts(string search)
        {
            var result = await _accountServices.SearchAccounts(search);
            if(result == null)
            {
                return BadRequest("Search cannot be empty");
            }
            return Ok(result);
        }


        [Authorize(Roles = UserRoles.User)]
        [HttpGet("ViewFollowing")]
        public async Task<ActionResult<IEnumerable<ViewAccountDTO>>> ViewFollowing(string username)
        {
            var result = await _accountServices.GetFollowing(username);
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet("ViewNotifications")]
        public async Task<ActionResult<ViewAllNotificationsDTO>> ViewNotifications()
        {
            var result = await _accountServices.ViewNotifications(User.Identity.Name);
            return Ok(result);
        }
    }
}
