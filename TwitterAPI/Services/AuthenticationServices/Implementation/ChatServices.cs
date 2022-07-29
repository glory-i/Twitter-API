using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.DTOs.ChatDTOs;
using TwitterAPI.DTOs.MessageDTOs;
using TwitterAPI.Repositories.Interfaces;
using TwitterAPI.Services.AuthenticationServices.Interface;

namespace TwitterAPI.Services.AuthenticationServices.Implementation
{
    public class ChatServices : IChatServices
    {
        private readonly IChatRepository _chatRepository;
        public ChatServices(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<IEnumerable<ViewMessageDTO>> GetChatHistory(string SenderUsername, string ReceiverUsername)
        {
            //throw new NotImplementedException();
            List<ViewMessageDTO> AllMessages = new List<ViewMessageDTO>();
            var messages = await _chatRepository.GetChatHistory(SenderUsername, ReceiverUsername);
            if(messages== null)
            {
                return null;
            }
            foreach(var message in messages)
            {
                ViewMessageDTO viewMessageDTO = new ViewMessageDTO
                {
                    SenderUsername = message.MessageSender,
                    MessageContent = message.Content,
                    DateSent = message.DateCreated.Date.ToString("dd-MMMM-yyyy"),
                    TimeSent = message.DateCreated.TimeOfDay.ToString()
                };

                AllMessages.Add(viewMessageDTO);
            }

            return AllMessages;
        }

        public async Task<string> SendMessage(string SenderUsername, string ReceiverUsername, CreateMessageDTO createMessageDTO)
        {
            var result = await _chatRepository.SendMessage(SenderUsername, ReceiverUsername, createMessageDTO.MessageContent);
            return result;

        }

        public async Task<IEnumerable<ViewChatDTO>> ViewAllChats(string username)
        {
            //throw new NotImplementedException();
            List<ViewChatDTO> ViewAllChats = new List<ViewChatDTO>();
            var AllChats = await _chatRepository.GetChatList(username);
            foreach(var chat in AllChats)
            {
                ViewChatDTO viewChatDTO = new ViewChatDTO
                {
                    Name = chat.Receiver,
                    LastMessageContent = chat.LastMessage.Content,
                    LastMessageDate = chat.LastMessage.DateCreated.Date.ToString("dd-MMMM-yyyy"),
                    LastMessageTime = chat.LastMessage.DateCreated.TimeOfDay.ToString()
                };
                if (chat.LastMessage.MessageSender == username)
                {
                    viewChatDTO.LastMessageContent = "You:" + viewChatDTO.LastMessageContent;
                }
                ViewAllChats.Add(viewChatDTO);

            }
            return ViewAllChats;
        }
    }
}
