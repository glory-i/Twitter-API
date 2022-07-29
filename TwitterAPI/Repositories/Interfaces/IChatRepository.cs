using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.DatabaseRepository.Interface;
using TwitterAPI.Model;

namespace TwitterAPI.Repositories.Interfaces
{
    public interface IChatRepository : IDatabaseRepository<Chat>
    {
        public void SaveChanges();

        public Task<string> SendMessage(string SenderUsername, string ReceiverUsername, string content);
        public Task<IEnumerable<Message>> GetChatHistory(string SenderUsername, string ReceiverUsername);

        public Task<IEnumerable<Chat>> GetChatList(string username);


    }
}
