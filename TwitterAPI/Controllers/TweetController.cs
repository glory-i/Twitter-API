using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterAPI.Authentication;
using TwitterAPI.DTOs.TweetDTOs;
using TwitterAPI.Services.AuthenticationServices.Interface;

namespace TwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private ITweetServices _tweetServices;
        public TweetController(ITweetServices tweetServices)
        {
            _tweetServices = tweetServices;
        }


        [Authorize(Roles = UserRoles.User)]
        [HttpPost("CreateTweet")]
        public async Task<IActionResult> CreateTweet(CreateTweetDTO createTweetDTO)
        {
            var result = await _tweetServices.CreateTweet(createTweetDTO, User.Identity.Name);
            return Ok(result);
        }


        [Authorize(Roles = UserRoles.User)]
        [HttpPost("ReplyTweet")]
        public async Task<IActionResult> ReplyTweet(CreateTweetDTO createTweetDTO, int tweetId)
        {
            //var result = await _tweetServices.CreateTweet(createTweetDTO, User.Identity.Name);
            var result = await _tweetServices.ReplyTweet(createTweetDTO, tweetId, User.Identity.Name);
            return Ok(result);
        }


        [Authorize(Roles = UserRoles.User)]
        [HttpGet("{id}")]
        public async Task<IActionResult> ViewTweet(int id)
        {
            var result = await _tweetServices.GetTweet(id);
            if(result == null)
            {
                return BadRequest("This tweet does not exist");
            }

            return Ok(result);
        }


        [Authorize(Roles = UserRoles.User)]
        [HttpGet("ViewTweets")]
        public async Task<ActionResult<IEnumerable<ViewTweetDTO2>>> ViewTweets(string username)
        {
            var result = await _tweetServices.GetTweets(username);
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet("ViewLikedTweets")]
        public async Task<ActionResult<IEnumerable<ViewTweetDTO2>>> ViewLikedTweets(string username)
        {
            var result = await _tweetServices.GetLikedTweets(username);
            return Ok(result);
        }



        [Authorize(Roles = UserRoles.User)]
        [HttpGet("ViewTimeLine")]
        public async Task<ActionResult<IEnumerable<ViewTweetDTO2>>> ViewTimeLine()
        {
            var result = await _tweetServices.GetTimeLine(User.Identity.Name);
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet("SearchTweets")]
        public async Task<ActionResult<IEnumerable<ViewTweetDTO2>>> SearchTweets(string search, string username,string order)
        {
            var result = await _tweetServices.SearchTweets(search,username,order);
            if(result == null)
            {
                return BadRequest("Search cannot be empty");
            }
            return Ok(result);
        }
        
    }
}
