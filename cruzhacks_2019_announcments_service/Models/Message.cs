using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cruzhacks_2019_announcments_service.Models
{
    public class Message
    {
        public string senderName { get; set; }
        public string timeStamp { get; set; }
        public string messageTitle { get; set; }
        public string messageBody { get; set; }
    }
}
