using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAPI.Model
{
    public class Notification
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }

        //public int NoNewNotifications { get; set; }
    }
}
