using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.DatabaseRepository.Interface;
using TwitterAPI.Model;

namespace TwitterAPI.Repositories.Interfaces
{
    public interface ITweetRepository : IDatabaseRepository<Tweet>
    {
        public void SaveChanges();
        public Task<string> CreateTweet(Tweet tweet,string username);
        public Task<string> ReplyTweet(int tweetId,string UserReplying, string messsage);
        public Task<Tweet> GetTweet(int tweetId);
        public Task<IEnumerable<Tweet>> GetReplies(int tweetId);
        
        public Task<IEnumerable<TimeLineTweet>> GetAllTweets(string username);
        public Task<IEnumerable<Tweet>> GetLikedTweets(string username);
        public Task<IEnumerable<Tweet>> SearchTweets(string search, string username,string order);
        public Task<IEnumerable<TimeLineTweet>> GetTimeLine(string username);
        




    }
}
