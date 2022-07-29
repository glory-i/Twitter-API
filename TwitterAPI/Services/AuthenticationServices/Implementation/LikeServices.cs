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
    public class LikeServices : ILikeServices
    {
        private ILikeRepository _likeRepository;

        public LikeServices(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<IEnumerable<ViewAccountDTO>> AccountsLikedBy(int tweetid)
        {
            var accounts = await _likeRepository.AccountsLikedBy(tweetid);
            if (accounts == null)
            {
                return null;
            }
            List<ViewAccountDTO> ViewAccounts = new List<ViewAccountDTO>();
            foreach(var account in accounts)
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

        public async Task<string> LikeTweet(string username, int tweetid)
        {
            //throw new NotImplementedException();
            var result = await _likeRepository.LikeTweet(username, tweetid);
            return result;
        }

        public async Task<string> UndoLike(string username, int tweetid)
        {
            //throw new NotImplementedException();
            var result = await _likeRepository.UndoLike(username, tweetid);
            return result;
        }
    }
}
