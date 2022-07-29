using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.DTOs.TweetDTOs;

namespace TwitterAPI.Services.AuthenticationServices.Interface
{
    public interface ITweetServices
    {
        public Task<string> CreateTweet(CreateTweetDTO createTweetDTO, string username);
        public Task<string> ReplyTweet(CreateTweetDTO createTweetDTO, int tweetId, string UserReplying);
        public Task<ViewTweetDTO> GetTweet(int tweetId);

        public Task<IEnumerable<ViewTweetDTO2>> GetTweets(string username);
        public Task<IEnumerable<ViewTweetDTO2>> SearchTweets(string search,string username,string order);
        public Task<IEnumerable<ViewTweetDTO2>> GetLikedTweets(string username);
        public Task<IEnumerable<ViewTweetDTO2>> GetTimeLine(string username);


    }
}
