using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.DTOs.ChatDTOs;
using TwitterAPI.DTOs.MessageDTOs;

namespace TwitterAPI.Services.AuthenticationServices.Interface
{
    public interface IChatServices
    {
        public Task<string> SendMessage(string SenderUsername, string ReceiverUsername, CreateMessageDTO createMessageDTO);

        public Task<IEnumerable<ViewMessageDTO>> GetChatHistory(string SenderUsername, string ReceiverUsername);

        public Task<IEnumerable<ViewChatDTO>> ViewAllChats(string username);

    }
}
