using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.DTOs.AccountDTOs;
using TwitterAPI.DTOs.Notifications;
using TwitterAPI.DTOs.TweetDTOs;
using TwitterAPI.Model;

namespace TwitterAPI.Services.AuthenticationServices.Interface
{
    public interface IAccountServices
    {
        public Task<string> FollowAccount(string follower, string followed);

        public Task<ViewAccountDTO> GetAccount(string username);
        public Task<IEnumerable<ViewAccountDTO>> GetFollowers(string username);
        public Task<IEnumerable<ViewAccountDTO>> GetFollowing(string username);
        public Task<IEnumerable<ViewAccountDTO>> SearchAccounts(string search);
        public Task<ViewAllNotificationsDTO> ViewNotifications(string username);


    }
}
