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
        SmtpClient server = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new System.Net.NetworkCredential("spotifyfree886@gmail.com", "Pony1234"),
            EnableSsl = true
        };

        /*
        public void inviteUser(string emailadres)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("spotifyfree886@gmail.com");
            mail.To.Add(emailadres);
            mail.Subject = "Test";
            mail.Body = "Mooie test";
            server.Send(mail);
        }
        */

        public void Send3DayNotificationEmail(ActionToComplete action)
        {

        }

        public void SendTodayNotificationEmail(ActionToComplete action, string token, int actionHistoryId)
        {

        }

        public void SendActionCompletedEmail(CompletedAction action)
        {

        }

        public void SendUncompletedTaskEmail(ActionToComplete action)
        {

        }

        private void SendEmail(string body, string receiver)
        {
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("spotifyfree886@gmail.com", "Pony1234@")
            };

            using (var message = new MailMessage("spotifyfree886@gmail.com", receiver))
            {
                message.Subject = "Test";
                message.Body = body;
                message.IsBodyHtml = true;
                smtp.Send(message);
            }
        }

        public async void RenderRazorViewToString()
        {
            var engine = new RazorLightEngineBuilder()
                .UseMemoryCachingProvider()
                .Build();

            string template = "Hello, @Model.Name. Welcome to RazorLight repository";

            string result = await engine.CompileRenderAsync("templateKey", template, new { Name = "John Doe" });
        }
    }
}
