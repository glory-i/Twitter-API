using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterAPI.DTOs.Notifications
{
    public class ViewAllNotificationsDTO
    {
        //public int NoNewNotifications { get; set; }
        public int NoNewNotifications { get; set; } = 0;

        //public string Alert { get; set; }
        public string Alert { get; set; } = "";
        //public List<ViewNotificationDTO> Notifications { get; set; }
        public List<ViewNotificationDTO> Notifications { get; set; } = new List<ViewNotificationDTO>();  
        
    }

}
