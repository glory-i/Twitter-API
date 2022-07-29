using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAPI.DTOs.ChatDTOs
{
    public class ViewChatDTO
    {
        public string Name { get; set; }
        public string LastMessageContent { get; set; }
        public string LastMessageDate { get; set; }
        public string LastMessageTime { get; set; }
    }
}
