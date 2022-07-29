using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAPI.Model
{
    public class AccountDetails
    {
        public int Id { get; set; }
        public Account account { get; set; }
        public List<Account> Followers { get; set; } = new List<Account>();
        public List<Account> Following { get; set; } = new List<Account>();
        public List<Tweet> Tweets { get; set; } = new List<Tweet>();
        public List<Tweet> LikedTweets { get; set; } = new List<Tweet>();
    }
}
