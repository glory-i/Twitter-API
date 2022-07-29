using Microsoft.EntityFrameworkCore;
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
    public class RetweetRepository: DatabaseRepository<Retweet>, IRetweetRepository
    {
        private readonly ApplicationDbContext _context;
        public RetweetRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
        }
        public async Task<string> RetweetTweet(string username, int tweetid)
        {
           //we check to ensure the tweet exists and get the account that is trying to retweet it
            
            var tweet = await _context.Tweets.FindAsync(tweetid);
            var account = _context.Accounts.Where(a => a.Username == username).FirstOrDefault();

            if (tweet == null)
            {
                return "The tweet does not exist";
            }
            
            //check to ensure the account has not already retweeted that particular tweet
            if (_context.Retweets.Where(r => r.Username == username && r.TweetId == tweetid).Any())
            {
                return "You have already retweeted this tweet";
            }

            Retweet retweet = new Retweet
            {
                TweetId = tweetid,
                Username = username,
                DateRetweeted = DateTime.Now
            };

            account.NoOfTweets++;  //increment the number of tweets of the account retweeting it
            tweet.NoOfRetweets++;  //increment the number of retweets of the tweet
            tweet.NoOfInteractions++;

            //send notification to original author of tweet that their tweet was retweeted
            var AuthorOFTweet = _context.Accounts.Where(a => a.Username == tweet.Username).FirstOrDefault();
            Notification notification = new Notification
            {
                AccountId = AuthorOFTweet.Id,
                Message = $"@{username} retweeted your tweet  ''{tweet.Message}''   ",
                Username = AuthorOFTweet.Username
            };
            await _context.Notifications.AddAsync(notification);

            AuthorOFTweet.NoNewNotifications++; //increment the no of new notifications the author of the tweet has




            //get the usernames of all the accounts that retweeted the tweet
            //use the usernames to get the accounts that retweeted the tweet and place them in a list
            var UsernamesAccountsRetweeted = _context.Retweets.Where(r => r.TweetId == tweetid).Select(r => r.Username).ToList();
            List<Account> AccountsRetweeted = new List<Account>();
            foreach (var usernameAccountRetweeted in UsernamesAccountsRetweeted)
            {
                var accountRetweeted = _context.Accounts.Where(a => a.Username == usernameAccountRetweeted).FirstOrDefault();
                AccountsRetweeted.Add(accountRetweeted);
            }



            // send notification to each account that retweeted the original tweet that their retweet was retweeted
            foreach (Account accountRetweeted in AccountsRetweeted)
            {
                Notification notification1 = new Notification
                {
                    AccountId = accountRetweeted.Id,
                    //Message = "@" + username + " retweeted your retweet " +" " + tweet.Message,
                    Message = $"@{username} retweeted your retweet : ''{tweet.Message}'' ",

                    Username = accountRetweeted.Username
                };
                await _context.Notifications.AddAsync(notification1);
                accountRetweeted.NoNewNotifications++;
            }


            await _context.Retweets.AddAsync(retweet);
            await _context.SaveChangesAsync();
            return "Successfully retweeted the tweet";
        }

        public async Task<string> UndoRetweet(string username, int tweetid)
        {
            var account = _context.Accounts.Where(a => a.Username == username).FirstOrDefault();
            var tweet = await _context.Tweets.FindAsync(tweetid);
            if (tweet == null)
            {
                return "This tweet does not exist";
            }

            var retweet = _context.Retweets.Where(l => l.Username == username && l.TweetId == tweetid).FirstOrDefault();
            if (retweet == null)
            {
                return "You cannot Undo Retweet from a Tweet You have not Retweeted";
            }

            tweet.NoOfRetweets--;
            tweet.NoOfInteractions--;
            account.NoOfTweets--;
            _context.Retweets.Remove(retweet);
            await _context.SaveChangesAsync();
            return "Sucessfully removed Retweet from this tweet";

        }

        public async Task<IEnumerable<Account>> AccountsRetweetedBy(int tweetid)
        {
            var tweet = await _context.Tweets.FindAsync(tweetid);
            if (tweet == null)
            {
                return null;
            }
            var Usernames = _context.Retweets.Where(l => l.TweetId == tweetid).Select(l => l.Username).ToList();
            List<Account> accounts = new List<Account>();

            foreach (var username in Usernames)
            {
                var account = _context.Accounts.Where(a => a.Username == username).FirstOrDefault();
                accounts.Add(account);
            }
            accounts.Reverse();
            return accounts;

        }

    }
}
