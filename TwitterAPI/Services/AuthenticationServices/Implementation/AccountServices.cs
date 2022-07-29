using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.DTOs.AccountDTOs;
using TwitterAPI.DTOs.Notifications;
using TwitterAPI.DTOs.TweetDTOs;
using TwitterAPI.Repositories.Interfaces;
using TwitterAPI.Services.AuthenticationServices.Interface;

namespace TwitterAPI.Services.AuthenticationServices.Implementation
{
    public class AccountServices : IAccountServices
    {
        private IAccountRepository _accountRepository;

        public AccountServices(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<string> FollowAccount(string follower, string followed)
        {
            //throw new NotImplementedException();
           var result = await _accountRepository.FollowAccount(follower, followed);
            if(result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public async Task<ViewAccountDTO> GetAccount(string username)
        {
            var account = await _accountRepository.GetAccount(username);
            if (account == null)
            {
                return null;
            }

            ViewAccountDTO viewAccountDTO = new ViewAccountDTO
            {
                Username = $"@{account.Username}",
                TwitterName = account.TwitterName,
                Bio = account.Bio,
                NoOfFollowers = account.NoOfFollowers,
                NoOfFollowing = account.NoOfFollowing,
                Birthday = account.Birthday.Date,
                DateCreated = account.DateCreated.Date,
                NoOfTweets = account.NoOfTweets,
                NoOfLikedTweets = account.NoOfLikedTweets
            };
            return viewAccountDTO;
        }

        public async Task<IEnumerable<ViewAccountDTO>> GetFollowers(string username)
        {
            //throw new NotImplementedException();
            List<ViewAccountDTO> Followers = new List<ViewAccountDTO>();
            var followers = await _accountRepository.GetAllFollowers(username);
            foreach(var follower in followers)
            {
                ViewAccountDTO viewAccountDTO = new ViewAccountDTO
                {
                    TwitterName = follower.TwitterName,
                    Username = follower.Username,
                    Bio = follower.Bio,
                    NoOfFollowers = follower.NoOfFollowers,
                    NoOfFollowing = follower.NoOfFollowing,
                    Birthday = follower.Birthday,
                    DateCreated = follower.DateCreated
                };
                Followers.Add(viewAccountDTO);
            }
            return Followers;
        }

        public async Task<IEnumerable<ViewAccountDTO>> GetFollowing(string username)
        {
            List<ViewAccountDTO> Following = new List<ViewAccountDTO>();
            var following = await _accountRepository.GetAllFollowing(username);
            foreach (var follow in following)
            {
                ViewAccountDTO viewAccountDTO = new ViewAccountDTO
                {
                    TwitterName = follow.TwitterName,
                    Username = follow.Username,
                    Bio = follow.Bio,
                    NoOfFollowers = follow.NoOfFollowers,
                    NoOfFollowing = follow.NoOfFollowing,
                    Birthday = follow.Birthday,
                    DateCreated = follow.DateCreated
                };
                Following.Add(viewAccountDTO);
            }
            return Following;
        }

       


        /*public async Task<ViewAllNotificationsDTO> ViewNotifications(string username)
        {

            var NoNewNotifications = await _accountRepository.GetNewNotifications(username);
            //get no of new notifications before getting notifications
            //so it won't reset to 0 (check GetNotifications in repository)

            var Notifications = await  _accountRepository.GetNotifications(username);
            List<ViewNotificationDTO> NotificationsList = new List<ViewNotificationDTO>();
            
            foreach(var Notification in Notifications)
            {
                ViewNotificationDTO viewNotificationDTO = new ViewNotificationDTO
                {
                    Message= Notification.Message
                };
                NotificationsList.Add(viewNotificationDTO);
            }

            NotificationsList.Reverse();

            //return await Task.FromResult(NotificationsList);
            
            
            ViewAllNotificationsDTO AllNotifications = new ViewAllNotificationsDTO
            {
                NoNewNotifications = NoNewNotifications,
                Alert = $"YOU HAVE {NoNewNotifications} NEW NOTIFICATION(S)",
                Notifications = NotificationsList
            };

            return AllNotifications;
        }*/

        

        public async Task<ViewAllNotificationsDTO> ViewNotifications(string username)
        {
            var NoNewNotifications = await _accountRepository.GetNewNotifications(username);
            var AlertMessage = $"YOU HAVE {NoNewNotifications} NEW NOTIFICATIONS";
            ViewAllNotificationsDTO AllNotifications = new ViewAllNotificationsDTO();

            var Notifications = await _accountRepository.GetNotifications(username);
            foreach(var notification in Notifications)
            {
                ViewNotificationDTO viewNotification = new ViewNotificationDTO
                {
                    Message = notification.Message
                };
                AllNotifications.Notifications.Add(viewNotification);
            }

            AllNotifications.Notifications.Reverse();
            AllNotifications.NoNewNotifications = NoNewNotifications;
            AllNotifications.Alert = AlertMessage;
            return AllNotifications;

        }


        public async Task<IEnumerable<ViewAccountDTO>> SearchAccounts(string search)
        {
            //throw new NotImplementedException();
            List<ViewAccountDTO> AccountsList = new List<ViewAccountDTO>();
            var accounts = await _accountRepository.SearchAccounts(search);
            if (accounts == null)
            {
                return null;
            }

            foreach (var account in accounts)
            {
                ViewAccountDTO viewAccountDTO = new ViewAccountDTO
                {
                    TwitterName = account.TwitterName,
                    Username = account.Username,
                    Bio = account.Bio,
                    NoOfFollowers = account.NoOfFollowers,
                    NoOfFollowing = account.NoOfFollowing,
                    Birthday = account.Birthday,
                    DateCreated = account.DateCreated
                };
                AccountsList.Add(viewAccountDTO);
            }
            return AccountsList;
        }
    }
}
