using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.Authentication;
using TwitterAPI.DatabaseRepository.Implementation;
using TwitterAPI.Model;
using TwitterAPI.Repositories.Interfaces;

namespace TwitterAPI.Repositories.Implementations
{
    public class AccountRepository : DatabaseRepository<Account>, IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
        }

        public async Task<String> FollowAccount(string follower, string followed)
        {
            //we have to check that you are not already following the person
            var isFollowing = _context.Follows.Where(f => f.Follower == follower && f.Followed == followed).Any();
            if (isFollowing)
            {
                return "You are already following this user";
            }

            if (followed == follower)
            {
                return "You cannot follow yourself";
            }
            // we have to increase the number of following of the follower
            // we have to increase trhe number of followers of the followed
            var FollowedAccount = _context.Accounts.Where(a => a.Username == followed).FirstOrDefault();
            if (FollowedAccount == null)
            {
                return null;
            }
            FollowedAccount.NoOfFollowers++;

            var FollowerAccount = _context.Accounts.Where(a => a.Username == follower).FirstOrDefault();
            FollowerAccount.NoOfFollowing++;

            Follow follow = new Follow
            {
                Followed = followed,
                Follower = follower,
            };

            Notification notification = new Notification
            {
                AccountId = FollowedAccount.Id,
                Message = $"@{follower} followed you",
                Username = FollowedAccount.Username
            };

            FollowedAccount.NoNewNotifications++; //increase number of new notifications for the account that was just followed

            await _context.Notifications.AddAsync(notification);
            await _context.Follows.AddAsync(follow);
            await _context.SaveChangesAsync();

            return $"Successfully Folllowed @{followed}";
        }



        public async Task<Account> GetAccount(string username)
        {
            //throw new NotImplementedException();
            var account = _context.Accounts.Where(a => a.Username == username).FirstOrDefault();
            if (account == null)
            {
                return null;
            }
            return await Task.FromResult(account);
        }

        public async Task<IEnumerable<Account>> GetAllFollowers(string username)
        {
            
            var FollowersUsernames = _context.Follows.Where(f => f.Followed == username).Select(f=>f.Follower).ToList();
            List<Account> Followers = new List<Account>();
            foreach(var followerUsername in FollowersUsernames)
            {
                var follower = _context.Accounts.Where(a => a.Username == followerUsername).FirstOrDefault();
                Followers.Add(follower);
            }
            Followers.Reverse();
            return await Task.FromResult(Followers);
        }

        public async Task<IEnumerable<Account>> GetAllFollowing(string username)
        {
            var FollowingUsernames = _context.Follows.Where(f => f.Follower == username).Select(f => f.Followed).ToList();
            List<Account> Following = new List<Account>();
            foreach (var followingUsername in FollowingUsernames)
            {
                var following = _context.Accounts.Where(a => a.Username == followingUsername).FirstOrDefault();
                Following.Add(following);
            }
            Following.Reverse();
            return await Task.FromResult(Following);
        }


        /*public async Task<IEnumerable<Tweet>> GetTimeLine(string username)
        {
            List<Tweet> AllTweets = new List<Tweet>();
            
            //add my tweets
            var MyTweets = await GetAllTweets(username);
            foreach(var mytweet in MyTweets)
            {
                AllTweets.Add(mytweet);
            }

            //add tweets of all the accounts you are following
            //1. get all following
            //2. loop through each following and get all the tweets of each following
            //3. loop through each tweet of each follower, check if it is in the tl, then add it
            //4.arrange the tl based on time 


            var AllFollowing = await GetAllFollowing(username);
            foreach (var following in AllFollowing)
            {
                var followingtweets = await GetAllTweets(following.Username);
                foreach(var tweet in followingtweets)
                {
                    if (!AllTweets.Contains(tweet))
                    {
                        AllTweets.Add(tweet);
                    }
                }
            }

            //order by date/time tweeted
            var FinalTimeLine = AllTweets.OrderByDescending(t => t.DateCreated);
            return FinalTimeLine;
        }*/



        public async Task<IEnumerable<Notification>> GetNotifications(string username)
        {
            //after showing your notifications you need to reset new notifications to 0.

            var notifications = _context.Notifications.Where(n => n.Username == username).ToList();
            
            var account = _context.Accounts.Where(a => a.Username == username).FirstOrDefault();
            account.NoNewNotifications = 0;
            await _context.SaveChangesAsync(); 
            
            return await Task.FromResult(notifications);
        }

        public async Task<int> GetNewNotifications(string username)
        {
            //get the number of new notifications for that account

            return await Task.FromResult(_context.Accounts.Where(a => a.Username == username).Select(a => a.NoNewNotifications).FirstOrDefault());
        }

        public async Task<IEnumerable<Account>> SearchAccounts(string search)
        {
            if (String.IsNullOrEmpty(search))
            {
                return null;
            }
            var accounts = _context.Accounts.AsQueryable();
            accounts = accounts.Where(a => (a.Bio + a.Username + a.TwitterName).Contains(search));
            var SearchResult = await Task.FromResult(accounts.ToList());
            return SearchResult;
        }
    }
}
