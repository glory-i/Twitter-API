using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAPI.Model
{
    public class Tweet
    {
        public int Id { get; set; }
      //  public int AccountId { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public int NoOfLikes { get; set; }
        public int NoOfRetweets { get; set; }
        public int NoOfReplies { get; set; }

        public int NoOfInteractions { get; set; } // noof(likes+retweets+replies)
       
        // public List<Tweet> ListOfReplies { get; set; } = new List<Tweet>();
        //public List<Account> AccountsLikedBy { get; set; } = new List<Account>();
        //public List<Account> AccountsRetweetedBy { get; set; } = new List<Account>();

    }
}
