using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.DatabaseRepository.Interface;
using TwitterAPI.Model;

namespace TwitterAPI.Repositories.Interfaces
{
    public interface IRetweetRepository : IDatabaseRepository<Retweet>
    {
        public void SaveChanges();
        public Task<string> RetweetTweet(string username, int tweetid); //go to retweets
        public Task<string> UndoRetweet(string username, int tweetid);
        public Task<IEnumerable<Account>> AccountsRetweetedBy(int tweetid);


    }
}
