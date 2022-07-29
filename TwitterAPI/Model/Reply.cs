using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAPI.Model
{
    public class Reply
    {
        public int Id { get; set; }
        public int tweetId { get; set; }
        public int ReplyTweetId { get; set; }
        public string UserReplying { get; set; }
        public string UserReplied { get; set; }
        public string Message { get; set; }
        
        //public int MyProperty { get; set; }
    }
}
