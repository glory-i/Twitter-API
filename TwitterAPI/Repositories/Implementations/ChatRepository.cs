using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.Authentication;
using TwitterAPI.DatabaseRepository.Implementation;
using TwitterAPI.Model;
using TwitterAPI.Repositories.Interfaces;

namespace TwitterAPI.Repositories.Implementations
{
    public class ChatRepository : DatabaseRepository<Chat>, IChatRepository
    {
        private readonly ApplicationDbContext _context;
        public ChatRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetChatHistory(string SenderUsername, string ReceiverUsername)
        {
            Chat ExistingChat = new Chat();
            bool ChatExists = false;

            //check if a chat between the 2 already exists
            var ChatExists1 = _context.Chats.Where(c => c.Sender == SenderUsername && c.Receiver == ReceiverUsername);
            var ChatExists2 = _context.Chats.Where(c => c.Receiver == SenderUsername && c.Sender == ReceiverUsername);

            if (ChatExists1.Any())
            {
                ExistingChat = ChatExists1.FirstOrDefault();
                ChatExists = true;
            }

            if (ChatExists2.Any())
            {
                ExistingChat = ChatExists2.FirstOrDefault();
                ChatExists = true;
            }


            if (!ChatExists)
            {
                return null;
            }
            var messages = _context.Messages.Where(m => m.ChatId == ExistingChat.Id).OrderByDescending(m => m.DateCreated).ToList();
            return await Task.FromResult(messages);
        }

        public async Task<IEnumerable<Chat>> GetChatList(string username)
        {
            var AllChats = _context.Chats.Where(c => c.Sender == username || c.Receiver == username).ToList();
            foreach(var chat in AllChats)
            {
                if (chat.Receiver == username)
                {
                    var temp = chat.Sender;
                    chat.Sender = username;
                    chat.Receiver = temp;
                }
                chat.LastMessage = await _context.Messages.FindAsync(chat.LastMessageId);
               
            }
            var ChatList = AllChats.OrderByDescending(c => c.LastMessage.DateCreated);
            return await Task.FromResult(ChatList);
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
        }

        public async Task<string> SendMessage(string SenderUsername, string ReceiverUsername, string content)
        {
            var LastMessage = _context.Messages.OrderBy(m=>m.Id).Last();
            Chat ExistingChat = new Chat();
            Chat LastChat = new Chat();
            
            bool ChatExists = false;

            //check f the sender is being followed
            var IsFollowing = _context.Follows.Where(f => f.Follower == ReceiverUsername && f.Followed == SenderUsername).Any();
            if (!IsFollowing)
            {
                return "You cannot send message to this user because this user does not follow you";
            }

            //check if a chat between the 2 already exists
            var ChatExists1 = _context.Chats.Where(c => c.Sender == SenderUsername && c.Receiver == ReceiverUsername);
            var ChatExists2 = _context.Chats.Where(c => c.Receiver == SenderUsername && c.Sender == ReceiverUsername);
            
            if(ChatExists1.Any())
            {
                ExistingChat = ChatExists1.FirstOrDefault();
                ChatExists = true;
            }

            if (ChatExists2.Any())
            {
                ExistingChat = ChatExists2.FirstOrDefault();
                ChatExists = true;
            }

            if (!ChatExists)
            {
                if (_context.Chats.Any())
                {
                   LastChat = _context.Chats.OrderBy(c => c.Id).Last();
                }

                Chat chat = new Chat
                {
                    NoOfMessages = 1,
                    Sender = SenderUsername,
                    Receiver = ReceiverUsername,
                    DateCreated = DateTime.Now,
                    LastMessageId = (LastMessage.Id)+1
                };

                Message message = new Message
                {
                    ChatId = (LastChat.Id + 1),
                    MessageSender = SenderUsername,
                    MessageReceiver = ReceiverUsername,
                    Content = content,
                    DateCreated = DateTime.Now
                };
                await _context.Chats.AddAsync(chat);
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();
                return "Message Successfully sent";

            }

            //if the chat between both of them already exists, just create new message

            Message NewMessage = new Message
            {
                ChatId = ExistingChat.Id,
                MessageSender = SenderUsername,
                MessageReceiver = ReceiverUsername,
                Content = content,
                DateCreated = DateTime.Now
            };

            ExistingChat.NoOfMessages++;
            ExistingChat.LastMessageId = (LastMessage.Id) + 1;
            await _context.Messages.AddAsync(NewMessage);
            await _context.SaveChangesAsync();
            return "Message Successfully sent";




        }
    }
}
