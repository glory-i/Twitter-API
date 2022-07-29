using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAPI.Model
{
    public class Chat
    {
        public int Id { get; set; }
        //public string NameOfChat { get; set; } = "";
        public int LastMessageId { get; set; }
        public virtual  Message LastMessage { get; set; }
       // public string Type { get; set; }
        public int NoOfMessages { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
