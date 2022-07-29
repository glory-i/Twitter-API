using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.DTOs.AccountDTOs;

namespace TwitterAPI.Services.AuthenticationServices.Interface
{
    public interface IRetweetServices
    {
        public Task<string> RetweetTweet(string username, int tweetid);
        public Task<string> UndoRetweet(string username, int tweetid);
        public Task<IEnumerable<ViewAccountDTO>> AccountsRetweetedBy(int tweetid);
    }
}
