using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Business
{
    class EmailLogic
    {
        SmtpClient server = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new System.Net.NetworkCredential("spotifyfree886@gmail.com", "Pony1234"),
            EnableSsl = true
        };

        public void inviteUser(string emailadres)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("spotifyfree886@gmail.com");
            mail.To.Add(emailadres);
            mail.Subject = "Test";
            mail.Body = "Mooie test";
            server.Send(mail);
        }
    }
}
