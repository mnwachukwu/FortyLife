using System.Net;
using System.Net.Mail;

namespace FortyLife.Core
{
    public class Mailer
    {
        private readonly SmtpClient mailClient = new SmtpClient("smtp.sparkpostmail.com", 587);

        public Mailer()
        {
            mailClient.Credentials = new NetworkCredential("SMTP_Injection", "acd0679101745638a5bd844adf6a3a2b2142080e");
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
                From = new MailAddress("noreply@forty.life", "Forty Life Administrator"),
                ReplyToList = { "noreply@forty.life" },
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml
            };

            mailClient.Send(mailMessage);
        }
    }
}
