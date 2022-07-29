using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAPI.Model
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string TwitterName { get; set; }
        public string Bio { get; set; }
        public int NoOfFollowers { get; set; }
        public int NoOfFollowing { get; set; }
        public int NoOfTweets { get; set; }
        public int NoOfLikedTweets { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime DateCreated { get; set; }

        public int NoNewNotifications { get; set; }

        //public List<Tweet> tweets { get; set; }
        // public List<int> FollowersId { get; set; } = new List<int>();
        //public List<int> LikedTweetsId { get; set; } = new List<int>();
        //public AccountDetails accountDetails { get; set; }

        /* public List<Account> Followers { get; set; } = new List<Account>();
         public List<Account> Following { get; set; } = new List<Account>();
         public List<Tweet> Tweets { get; set; } = new List<Tweet>();
         public List<Tweet> LikedTweets { get; set; } = new List<Tweet>();  */

    }
}
