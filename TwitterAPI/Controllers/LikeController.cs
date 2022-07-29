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
    public class LikeController : ControllerBase
    {
        private ILikeServices _likeServices;

        public LikeController(ILikeServices likeServices)
        {
            _likeServices = likeServices;
        }


        [Authorize(Roles = UserRoles.User)]
        [HttpPost("LikeTweet")]
        public async Task<ActionResult> LikeTweet(int tweetid)
        {
            var result = await _likeServices.LikeTweet(User.Identity.Name, tweetid);
            return Ok(result);
        }


        [Authorize(Roles = UserRoles.User)]
        [HttpPost("UndoLike")]
        public async Task<ActionResult> UndoLike(int tweetid)
        {
            var result = await _likeServices.UndoLike(User.Identity.Name, tweetid);
            return Ok(result);
        }


        [Authorize(Roles = UserRoles.User)]
        [HttpGet("AccountsLikedBy")]
        public async Task<ActionResult> GetAccountsLikedBy(int tweetid)
        {
            var result = await _likeServices.AccountsLikedBy(tweetid);
            if(result == null)
            {
                return BadRequest("THIS TWEET DOES NOT EXIST");
            }

            return Ok(result);
        }
    }
}
