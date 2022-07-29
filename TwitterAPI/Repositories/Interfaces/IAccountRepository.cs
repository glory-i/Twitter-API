using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.DatabaseRepository.Interface;
using TwitterAPI.Model;

namespace TwitterAPI.Repositories.Interfaces
{
    public interface IAccountRepository : IDatabaseRepository<Account>
    {
        public void SaveChanges();
        public Task<string> FollowAccount(string follower, string followed);

        public Task<Account> GetAccount(string username);

        //public Task<IEnumerable<Tweet>> GetAllTweets(string username);
        
        //public Task<IEnumerable<Tweet>> GetTimeLine(string username);

        public Task<IEnumerable<Account>> GetAllFollowers(string username);
        public Task<IEnumerable<Account>> GetAllFollowing(string username);
        public Task<IEnumerable<Account>> SearchAccounts(string search);
        public Task<IEnumerable<Notification>> GetNotifications(string username);
        public Task<int> GetNewNotifications(string username);

    }
}
