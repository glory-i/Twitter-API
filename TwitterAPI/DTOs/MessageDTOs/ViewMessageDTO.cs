using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAPI.DTOs.MessageDTOs
{
    public class ViewMessageDTO
    {
        public string SenderUsername { get; set; }
        public string MessageContent { get; set; }
        public string DateSent { get; set; }
        public string TimeSent { get; set; }
    }
}
