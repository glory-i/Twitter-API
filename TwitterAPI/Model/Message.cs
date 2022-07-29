using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAPI.Model
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string MessageSender { get; set; }
        public string MessageReceiver { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
