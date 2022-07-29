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
    public class TweetRepository : DatabaseRepository<Tweet>, ITweetRepository
    {
        private readonly ApplicationDbContext _context;
        public TweetRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<string> CreateTweet(Tweet tweet,string username)
        {
            await this.Insert(tweet);
            var TweetOwner = _context.Accounts.Where(a => a.Username == username).FirstOrDefault();
            TweetOwner.NoOfTweets++;
            _context.SaveChanges();
            return ("Tweet successfully sent");
      
        }

        public async Task<IEnumerable<Tweet>> GetReplies(int tweetId)
        {
            //throw new NotImplementedException();
            var tweet = await _context.Tweets.FindAsync(tweetId);
            if (tweet == null)
            {
                return null;
            }
            var replies = _context.Replies.Where(r => r.tweetId == tweetId).ToList();
            List<Tweet> TweetReplies = new List<Tweet>();
            foreach(var reply in replies)
            {
                var TweetReply = await _context.Tweets.FindAsync(reply.ReplyTweetId);
                TweetReplies.Add(TweetReply);
            } 
            return TweetReplies;
        }

        public async Task<Tweet> GetTweet(int tweetId)
        {

            //var account = _context.Accounts.Where(a => a.Username == username).FirstOrDefault();
            var tweet = await _context.Tweets.FindAsync(tweetId);
            if (tweet == null)
            {
                return null;
            }
            return await Task.FromResult(tweet);
        }

        public async Task<string> ReplyTweet(int tweetId, string UserReplying, string messsage)
        {
            //throw new NotImplementedException();
            var LastTweet =  _context.Tweets.OrderBy(t=>t.Id).Last();
            var OriginalTweet = await _context.Tweets.FindAsync(tweetId);
            if(OriginalTweet == null)
            {
                return "This tweet does not exist";
            }
            OriginalTweet.NoOfReplies++;
            OriginalTweet.NoOfInteractions++;
            var UserReplied = _context.Accounts.Where(a => a.Username == OriginalTweet.Username).FirstOrDefault();
            var UserReplyingAccount = _context.Accounts.Where(a => a.Username == UserReplying).FirstOrDefault();


            Tweet NewTweet = new Tweet
            {
                Username = UserReplying,
                Message = $"@{UserReplied.Username} {messsage}",
                DateCreated = DateTime.Now,
                NoOfLikes = 0,
                NoOfRetweets = 0,
                NoOfReplies = 0
            };
            _context.Tweets.Add(NewTweet);

            Reply reply = new Reply
            {
                tweetId = OriginalTweet.Id,
                UserReplying = UserReplying,
                UserReplied = UserReplied.Username,
                ReplyTweetId = LastTweet.Id+1,
                Message = $"@{UserReplied.Username} {messsage}"
            };
            _context.Replies.Add(reply);

            Notification notification = new Notification
            {
                AccountId = UserReplied.Id,
                Message = $"@{UserReplying} replied your tweet:  {reply.Message}",
                Username = UserReplied.Username
            };

            UserReplied.NoNewNotifications++; //increment no of new notifications for the user that was replied
            UserReplyingAccount.NoOfTweets++; //increment number of tweets for the person that sent the reply

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return "Successfully replied tweet";
        }


        public async Task<IEnumerable<TimeLineTweet>> GetAllTweets(string username)
        {

            List<TimeLineTweet> ListOfTweets = new List<TimeLineTweet>();

            //get all the tweets of the user and turn it into a TimeLine tweet
            //add each of the TimeLine tweets to the list

            var tweets1 = _context.Tweets.Where(t => t.Username == username).ToList();
            foreach (var tweet in tweets1)
            {
                TimeLineTweet timeLineTweet = new TimeLineTweet
                {
                    Id = tweet.Id,
                    Username = tweet.Username,
                    Message = tweet.Message,
                    DateCreated = tweet.DateCreated,
                    NoOfLikes = tweet.NoOfLikes,
                    NoOfReplies = tweet.NoOfReplies,
                    NoOfRetweets = tweet.NoOfRetweets,
                    info = ""
                };
                ListOfTweets.Add(timeLineTweet);
            }

            //get all the retweets of the user and turn it into a TimeLine tweet
            //change the date the tweet was created to the time the user retweeted the tweet because in a sense that is the time the tweet was "created" by the user that retweeted it
            //add each of the TimeLine tweets to the list

            var retweets = _context.Retweets.Where(t => t.Username == username).ToList();
            foreach (var retweet in retweets)
            {
                var tweet = await _context.Tweets.FindAsync(retweet.TweetId);
                tweet.DateCreated = retweet.DateRetweeted;

                TimeLineTweet timeLineTweet = new TimeLineTweet
                {
                    Id = tweet.Id,
                    Username = tweet.Username,
                    Message = tweet.Message,
                    DateCreated = tweet.DateCreated,
                    NoOfLikes = tweet.NoOfLikes,
                    NoOfReplies = tweet.NoOfReplies,
                    NoOfRetweets = tweet.NoOfRetweets,
                    info = $"{retweet.Username} Retweeted"
                };
                ListOfTweets.Add(timeLineTweet);
                //tweets1.Add(tweet);

            }

            //order the tweets based on time (latest tweets on top).
            var alltweets = ListOfTweets.OrderByDescending(t => t.DateCreated);
            return alltweets;

        }

        public async Task<IEnumerable<Tweet>> GetLikedTweets(string username)
        {
            List<Tweet> LikedTweets = new List<Tweet>();
            var LikedTweetIds = _context.Likes.Where(l => l.Username == username).Select(l=>l.TweetId).ToList();
            foreach(var id in LikedTweetIds)
            {
                var tweet = await _context.Tweets.FindAsync(id);
                LikedTweets.Add(tweet);
            }
            LikedTweets.Reverse();
            return LikedTweets;
            

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

        public async Task<IEnumerable<TimeLineTweet>> GetTimeLine(string username)
        {
            List<TimeLineTweet> AllTweets = new List<TimeLineTweet>();

            //add my tweets
            var MyTweets = await GetAllTweets(username);
            foreach (var mytweet in MyTweets)
            {
                if (!AllTweets.Where(t => t.Id == mytweet.Id).Any())
                {
                    AllTweets.Add(mytweet);
                }
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
                foreach (var tweet in followingtweets)
                {
                    if (!AllTweets.Where(t => t.Id == tweet.Id).Any())
                    {
                        AllTweets.Add(tweet);
                    }
                }
            }

            //order by date/time tweeted
            var FinalTimeLine = AllTweets.OrderByDescending(t => t.DateCreated);
            return FinalTimeLine;
        }
        public override void SaveChanges()
        {
            base.SaveChanges();
        }

        public async Task<IEnumerable<Tweet>> SearchTweets(string search,string username,string order)
        {
            

            if (String.IsNullOrEmpty(search))
            {
                return null;
            }
            if (String.IsNullOrEmpty(order) || order.ToLower()!="latest")
            {
                order = "top";
            }

            search = search.ToLower();
            order = order.ToLower();
            var tweets = _context.Tweets.Where(t => t.Message.ToLower().Contains(search)).AsQueryable();
            if (!String.IsNullOrEmpty(username))
            {
                tweets = tweets.Where(t => t.Username == username);
            }
            

            List<Tweet> SearchResult = new List<Tweet>();
            if(order == "top")
            {
                SearchResult = tweets.OrderByDescending(t => t.NoOfInteractions).ToList();
            }

            if (order == "latest")
            {
                SearchResult = tweets.OrderByDescending(t => t.DateCreated).ToList();
            }

            return await Task.FromResult(SearchResult);

        }
    }
}
