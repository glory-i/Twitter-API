using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAPI.Model
{
    public class Retweet
    {
        public int Id { get; set; }
        public int TweetId { get; set; }
        public string Username { get; set; }

        public DateTime DateRetweeted { get; set; }
        //public List<Tweet> tweets { get; set; }
    }
}
