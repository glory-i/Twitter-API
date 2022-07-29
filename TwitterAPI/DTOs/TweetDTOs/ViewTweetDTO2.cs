using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAPI.DTOs.TweetDTOs
{
    public class ViewTweetDTO2
    {
        public string info { get; set; } = "";
        public string Username { get; set; }
        public string Message { get; set; }
       // public string DateCreated { get; set; }
       // public string TimeCreated { get; set; }
        public int NoOfLikes { get; set; }
        public int NoOfRetweets { get; set; }
        public int NoOfReplies { get; set; }

    }
}
