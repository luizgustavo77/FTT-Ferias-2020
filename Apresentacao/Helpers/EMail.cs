using Apresentacao.Helpers.Common;
using System;
using System.Net;
using System.Net.Mail;

namespace Apresentacao.Helpers
{
    public class EMail
    {
       

        public static void Send(string to, string title, string body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(State.SenderEmail);
                    mail.To.Add(to);
                    mail.Subject = title;
                    mail.Body = body;

                    using (SmtpClient smtp = new SmtpClient(State.SMTPServer, State.SMTPPort))
                    {
                        smtp.Credentials = new NetworkCredential(State.SenderEmail, new appSettings().Get("emailPassword"));
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}