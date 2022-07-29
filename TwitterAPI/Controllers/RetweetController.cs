using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterAPI.Authentication;
using TwitterAPI.Services.AuthenticationServices.Interface;

namespace TwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetweetController : ControllerBase
    {
        private IRetweetServices _retweetServices;

        public RetweetController(IRetweetServices retweetServices)
        {
            _retweetServices = retweetServices;
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpPost("RetweetTweet")]
        public async Task<ActionResult> RetweetTweet(int tweetid)
        {
            var result = await _retweetServices.RetweetTweet(User.Identity.Name, tweetid);
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpPost("UndoRetweet")]
        public async Task<ActionResult> UndoRetweet(int tweetid)
        {
            var result = await _retweetServices.UndoRetweet(User.Identity.Name, tweetid);
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet("AccountsRetweetedBy")]
        public async Task<ActionResult> GetAccountsRetweetedBy(int tweetid)
        {
            var result = await _retweetServices.AccountsRetweetedBy(tweetid);
            if (result == null)
            {
                return BadRequest("THIS TWEET DOES NOT EXIST");
            }

            return Ok(result);
        }

    }
}
