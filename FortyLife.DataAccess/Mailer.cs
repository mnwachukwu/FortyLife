using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace FortyLife.DataAccess
{
    public class Mailer
    {
        private readonly SmtpClient mailClient = new SmtpClient("mail.tm14.net", 587);

        public Mailer()
        {
            mailClient.Credentials = new NetworkCredential("automailer@mail.tm14.net", GetSmtpPassword());
        }

        /// <summary>
        /// Compose and send a mail to be sent by an smtp client connection.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        public void SendMail(string to, string subject, string body, bool isBodyHtml = true)
        {
            var mailMessage = new MailMessage
            {
                To = { to },
                Bcc = { "automailer@mail.tm14.net" },
                From = new MailAddress("no-reply@mail.tm14.life", "Forty Life Administrator"),
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml
            };

            mailClient.Send(mailMessage);
        }

        private static string GetSmtpPassword()
        {
            var path = HttpRuntime.AppDomainAppPath + @"\App_Data\smtp-pwd.txt";

            if (File.Exists(path)) return File.ReadAllText(path);

            // Didn't find a file, so make one - throw an error to inform the programmer/user
            File.AppendAllText(path, "Replace the contents of this file with a password to access a mail sever!");
            throw new Exception("The password file smtp-pwd.txt in App_Data didn't exist, but now it does. " +
                                "Replace its contents with an smtp password (NO EXTRA WHITESPACE) and try running this web app again.");
        }
    }
}
