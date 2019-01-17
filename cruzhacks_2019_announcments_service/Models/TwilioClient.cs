using System;
using System.Collections.Generic;
using System.Linq;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace cruzhacks_2019_announcments_service.Models
{
    public class TwilioAnnouncmentClient
    {
        private List<string> _phoneNumbers = new List<string>();
        private string _accountNumber;
        private string _accountSid;
        private string _authToken;

        public TwilioAnnouncmentClient()
        {
            _accountSid = Environment.GetEnvironmentVariable("TWILIO_ID");
            _authToken = Environment.GetEnvironmentVariable("TWILIO_TOKEN");
            _accountNumber = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_PHONE_NUMBER");
            TwilioClient.Init(_accountSid, _authToken);

            // CruzHacks Organizers

            if (Environment.GetEnvironmentVariable("DEV_ENVIRONMENT") == "DEV")
            {
                _phoneNumbers.Add("(707) 292 - 1668");
                /*
                _phoneNumbers.Add("8583959823");
                _phoneNumbers.Add("4088938387");
                _phoneNumbers.Add("4083327667");
                _phoneNumbers.Add("3106581281");
                _phoneNumbers.Add("8609188592");
                _phoneNumbers.Add("9092670121");
                _phoneNumbers.Add("4082186135");
                _phoneNumbers.Add("4083329644");
                */
            }
            else
            {
                // Get Live Hackers Numbers
            }
        }

        public void SendAnnouncment(string messageTitle, string messageBody)
        {
            var twilioAccountNumber = new Twilio.Types.PhoneNumber(_accountNumber);
            string formattedMessage = _FormatAnnouncment(messageTitle, messageBody);

            foreach (string number in _phoneNumbers)
            {
                try
                {
                    string formattedNumber = Regex.Replace(number, "[^0-9]", "");

                    var message = MessageResource.Create(
                        body: formattedMessage,
                        from: twilioAccountNumber,
                        to: new Twilio.Types.PhoneNumber(formattedNumber)
                    );
                    Console.WriteLine(message.Sid);
                }
                catch (Twilio.Exceptions.ApiException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private string _FormatAnnouncment(string messageTitle, string messageBody)
        {
            string helpInfo = "----\nReply STOP to unsubscribe from further notifications.";
            return "CruzHacks Hacker Announcment \n\n" + messageTitle + "\n\n" + messageBody + "\n\n" + helpInfo;
        }
    }
}
