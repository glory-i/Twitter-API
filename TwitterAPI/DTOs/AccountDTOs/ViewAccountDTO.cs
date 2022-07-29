using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAPI.DTOs.AccountDTOs
{
    public class ViewAccountDTO
    {
        //public int Id { get; set; }
        public string Username { get; set; }
        //public string Password { get; set; }
        //public string Email { get; set; }
        public string TwitterName { get; set; }
        public string Bio { get; set; }
        public int NoOfFollowers { get; set; }
        public int NoOfFollowing { get; set; }
        public int NoOfTweets { get; set; }
        public int NoOfLikedTweets { get; set; }
        //public int NoOfTweets { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
