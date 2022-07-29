using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.DTOs.AccountDTOs;

namespace TwitterAPI.Services.AuthenticationServices.Interface
{
    public interface ILikeServices
    {
        public Task<string> LikeTweet(string username, int tweetid);
        public Task<string> UndoLike(string username, int tweetid);
        public Task<IEnumerable<ViewAccountDTO>> AccountsLikedBy(int tweetid);

    
    }
}
