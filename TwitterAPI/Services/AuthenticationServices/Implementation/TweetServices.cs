using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.Authentication;
using TwitterAPI.DTOs.TweetDTOs;
using TwitterAPI.Model;
using TwitterAPI.Repositories.Interfaces;
using TwitterAPI.Services.AuthenticationServices.Interface;

namespace TwitterAPI.Services.AuthenticationServices.Implementation
{
    public class TweetServices : ITweetServices
    {
        private ITweetRepository _tweetRepository;

        public TweetServices(ITweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }

        
        public Task<string> CreateTweet(CreateTweetDTO createTweetDTO, string username)
        {
            //throw new NotImplementedException();
            Tweet tweet = new Tweet
            {
                Username = username,
                Message = createTweetDTO.Message,
                DateCreated = DateTime.Now,
                NoOfLikes = 0,
                NoOfReplies = 0,
                NoOfRetweets = 0
            };
            var result = _tweetRepository.CreateTweet(tweet, username);
            return result;
        }

        public async Task<ViewTweetDTO> GetTweet(int tweetId)
        {
            //throw new NotImplementedException();
            var tweet = await _tweetRepository.GetTweet(tweetId);
            if (tweet == null)
            {
                return null;
            }
            List<ViewTweetDTO2> Replies = new List<ViewTweetDTO2>();
            var TweetReplies = await _tweetRepository.GetReplies(tweetId);
            foreach(var reply in TweetReplies)
            {
                ViewTweetDTO2 viewTweetDTO2 = new ViewTweetDTO2
                {
                    Username = reply.Username,
                    Message = reply.Message,
                    NoOfLikes = reply.NoOfLikes,
                    NoOfRetweets = reply.NoOfRetweets
                };
                Replies.Add(viewTweetDTO2);
            }


            ViewTweetDTO viewTweetDTO = new ViewTweetDTO
            {
                Username = tweet.Username,
                Message = tweet.Message,
                DateCreated = tweet.DateCreated.Date.ToString("MM/dd/yyyy"),
                TimeCreated = tweet.DateCreated.TimeOfDay.ToString(),
                NoOfLikes = tweet.NoOfLikes,
                NoOfRetweets = tweet.NoOfRetweets,
                NoOfReplies = tweet.NoOfReplies,
                Replies = Replies
                
            };
            return viewTweetDTO;

        }

        public async Task<string> ReplyTweet(CreateTweetDTO createTweetDTO, int tweetId, string UserReplying)
        {
            //throw new NotImplementedException();
            var result = await _tweetRepository.ReplyTweet(tweetId, UserReplying, createTweetDTO.Message);
            return result;
        }

        public async Task<IEnumerable<ViewTweetDTO2>> GetTweets(string username)
        {
            //throw new NotImplementedException();
            var AllTweets = await _tweetRepository.GetAllTweets(username);
            List<ViewTweetDTO2> viewTweetDTO2s = new List<ViewTweetDTO2>();
            foreach (var tweet in AllTweets)
            {
                ViewTweetDTO2 viewTweetDTO2 = new ViewTweetDTO2
                {
                    info = tweet.info,
                    Username = tweet.Username,
                    Message = tweet.Message,
                    NoOfLikes = tweet.NoOfLikes,
                    NoOfRetweets = tweet.NoOfRetweets,
                    NoOfReplies = tweet.NoOfReplies
                    
                };
                viewTweetDTO2s.Add(viewTweetDTO2);
            }

            return viewTweetDTO2s;
        }


        public async Task<IEnumerable<ViewTweetDTO2>> GetLikedTweets(string username)
        {

            var AllTweets = await _tweetRepository.GetLikedTweets(username);
            List<ViewTweetDTO2> viewTweetDTO2s = new List<ViewTweetDTO2>();
            foreach (var tweet in AllTweets)
            {
                ViewTweetDTO2 viewTweetDTO2 = new ViewTweetDTO2
                {
                    info = $"Liked by {username}",
                    Username = tweet.Username,
                    Message = tweet.Message,
                    NoOfLikes = tweet.NoOfLikes,
                    NoOfRetweets = tweet.NoOfRetweets,
                    NoOfReplies = tweet.NoOfReplies

                };
                viewTweetDTO2s.Add(viewTweetDTO2);
            }

            return viewTweetDTO2s;
        }


        public async Task<IEnumerable<ViewTweetDTO2>> GetTimeLine(string username)
        {
            //throw new NotImplementedException();
            var AllTweets = await _tweetRepository.GetTimeLine(username);
            List<ViewTweetDTO2> viewTweetDTO2s = new List<ViewTweetDTO2>();
            foreach (var tweet in AllTweets)
            {
                ViewTweetDTO2 viewTweetDTO2 = new ViewTweetDTO2
                {
                    info = tweet.info,
                    Username = tweet.Username,
                    Message = tweet.Message,
                    NoOfLikes = tweet.NoOfLikes,
                    NoOfRetweets = tweet.NoOfRetweets,
                    NoOfReplies = tweet.NoOfReplies
                };
                viewTweetDTO2s.Add(viewTweetDTO2);
            }

            return viewTweetDTO2s;
        }

        public async Task<IEnumerable<ViewTweetDTO2>> SearchTweets(string search, string username, string order)
        {
            //throw new NotImplementedException();
            var Tweets = await _tweetRepository.SearchTweets(search, username,order);
            if (Tweets == null)
            {
                return null;
            }
            List<ViewTweetDTO2> SearchResults = new List<ViewTweetDTO2>();

            foreach(var tweet in Tweets)
            {
                ViewTweetDTO2 viewTweetDTO2 = new ViewTweetDTO2
                {
                    Username = tweet.Username,
                    Message = tweet.Message,
                    NoOfLikes = tweet.NoOfLikes,
                    NoOfRetweets = tweet.NoOfRetweets,
                    NoOfReplies = tweet.NoOfReplies
                };
                SearchResults.Add(viewTweetDTO2);
            }

            return SearchResults;
        }
    }
}
