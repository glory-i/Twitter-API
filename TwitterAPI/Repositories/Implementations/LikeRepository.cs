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
    public class LikeRepository : DatabaseRepository<Like>, ILikeRepository
    {
        private readonly ApplicationDbContext _context;
        public LikeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
        }

        public async Task<string> LikeTweet(string username, int tweetid)
        {
            //we check to ensure the tweet exists and get the account that is trying to like it

            var tweet = await _context.Tweets.FindAsync(tweetid);
            var account = _context.Accounts.Where(a => a.Username == username).FirstOrDefault();

            if (tweet == null)
            {
                return "The tweet does not exist";
            }

            //check to ensure the account has not already retweeted that particular tweet
            if (_context.Likes.Where(r => r.Username == username && r.TweetId == tweetid).Any())
            {
                return "You have already liked this tweet";
            }

            Like like = new Like
            {
                TweetId = tweetid,
                Username = username,
                DateLiked = DateTime.Now
            };

            account.NoOfLikedTweets++;  //increment the number of liked tweets of the account liking it
            tweet.NoOfLikes++;  //increment the number of likes of the tweet
            tweet.NoOfInteractions++;

            //send notification to original author of tweet that their tweet was liked
            var AuthorOFTweet = _context.Accounts.Where(a => a.Username == tweet.Username).FirstOrDefault();
            Notification notification = new Notification
            {
                AccountId = AuthorOFTweet.Id,
                Message = $"@{username} liked your tweet  ''{tweet.Message}''   ",
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



            // send notification to each account that retweeted the original tweet that their retweet was liked
            foreach (Account accountRetweeted in AccountsRetweeted)
            {
                Notification notification1 = new Notification
                {
                    AccountId = accountRetweeted.Id,
                    //Message = "@" + username + " retweeted your retweet " +" " + tweet.Message,
                    Message = $"@{username} liked your retweet : ''{tweet.Message}'' ",

                    Username = accountRetweeted.Username
                };
                await _context.Notifications.AddAsync(notification1);
                accountRetweeted.NoNewNotifications++;
            }


            await _context.Likes.AddAsync(like);
            await _context.SaveChangesAsync();
            return "Successfully liked the tweet";
        }

        public async Task<string> UndoLike(string username, int tweetid)
        {
            var account = _context.Accounts.Where(a => a.Username == username).FirstOrDefault();
            var tweet = await _context.Tweets.FindAsync(tweetid);
            if (tweet == null)
            {
                return "This tweet does not exist";
            }

            var like = _context.Likes.Where(l => l.Username == username && l.TweetId == tweetid).FirstOrDefault();
            if (like == null)
            {
                return "You cannot Remove Like from a Tweet You have not liked";
            }

            tweet.NoOfLikes--;
            tweet.NoOfInteractions--;
            account.NoOfLikedTweets--;
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
            return "Successfully Removed Like from this tweet";
        
        }

        public async Task<IEnumerable<Account>> AccountsLikedBy(int tweetid)
        {
            var tweet = await _context.Tweets.FindAsync(tweetid);
            if (tweet == null)
            {
                return null;
            }
            var Usernames = _context.Likes.Where(l => l.TweetId == tweetid).Select(l=>l.Username).ToList();
            List<Account> accounts = new List<Account>();

            foreach(var username in Usernames)
            {
                var account = _context.Accounts.Where(a => a.Username == username).FirstOrDefault();
                accounts.Add(account);
            }
            accounts.Reverse();
            return accounts;

        }

    }
}
