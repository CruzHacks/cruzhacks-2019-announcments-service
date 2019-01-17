using System.Collections.Generic;

namespace cruzhacks_2019_announcments_service.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string senderName { get; set; }
        public string timeStamp { get; set; }
        public string messageTitle { get; set; }
        public string messageBody { get; set; }

        public Dictionary<string, string> ToDictionary()
        {
            var response = new Dictionary<string, string>();
            response.Add("senderName", senderName);
            response.Add("senderName", timeStamp);
            response.Add("senderName", messageTitle);
            response.Add("senderName", messageBody);
            return response;
        }
    }
}