using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using RazorLight;

namespace Business
{
    public class EmailLogic
    {
        private readonly SmtpClient _smtpClient = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("spotifyfree886@gmail.com", "Pony1234@")
        };

        public void SendEmail(string title, string body, string receiver)
        {
            using (var message = new MailMessage("spotifyfree886@gmail.com", receiver))
            {
                message.Subject = title;
                message.Body = body;
                message.IsBodyHtml = true;
                _smtpClient.Send(message);
            }
        }
    }
}
