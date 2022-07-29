using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.DTOs.AccountDTOs;
using TwitterAPI.Repositories.Interfaces;
using TwitterAPI.Services.AuthenticationServices.Interface;

namespace TwitterAPI.Services.AuthenticationServices.Implementation
{
    public class RetweetServices : IRetweetServices
    {
        private IRetweetRepository _retweetRepository;

        public RetweetServices(IRetweetRepository retweetRepository)
        {
            _retweetRepository = retweetRepository;
        }

        public async Task<string> RetweetTweet(string username, int tweetid)
        {
            //throw new NotImplementedException();
            var result = await _retweetRepository.RetweetTweet(username, tweetid);
            return result;
        }

        public async Task<string> UndoRetweet(string username, int tweetid)
        {
            //throw new NotImplementedException();
            var result = await _retweetRepository.UndoRetweet(username, tweetid);
            return result;
        }

        public async Task<IEnumerable<ViewAccountDTO>> AccountsRetweetedBy(int tweetid)
        {
            var accounts = await _retweetRepository.AccountsRetweetedBy(tweetid);
            if (accounts == null)
            {
                return null;
            }
            List<ViewAccountDTO> ViewAccounts = new List<ViewAccountDTO>();
            foreach (var account in accounts)
            {
                ViewAccountDTO viewAccount = new ViewAccountDTO
                {
                    Username = account.Username,
                    TwitterName = account.TwitterName,
                    Bio = account.Bio,
                    NoOfFollowers = account.NoOfFollowers,
                    NoOfFollowing = account.NoOfFollowing,
                    NoOfLikedTweets = account.NoOfLikedTweets,
                    NoOfTweets = account.NoOfTweets,
                    Birthday = account.Birthday,
                    DateCreated = account.DateCreated
                };
                ViewAccounts.Add(viewAccount);
            }
            return ViewAccounts;
        }

    }
}
