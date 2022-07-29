using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterAPI.Model
{
    public class Like
    {
        public int Id { get; set; }
        public int TweetId { get; set; }
        public string Username { get; set; }

        public DateTime DateLiked { get; set; }
    }
}
