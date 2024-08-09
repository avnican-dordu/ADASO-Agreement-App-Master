using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace ADASO_AgreementApp.Controllers
{
    public class EmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUserName;
        private readonly string _smtpPassword;

        public EmailService(string smtpHost, int smtpPort, string smtpUserName, string smtpPassword)
        {
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _smtpUserName = smtpUserName;
            _smtpPassword = smtpPassword;
        }

        public void SendMail(string to, string subject, string body)
        {
            try
            {
                using (var message = new MailMessage(_smtpUserName, to))
                {
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;

                    using (var client = new SmtpClient(_smtpHost, _smtpPort))
                    {
                        // SSL/TLS kullanmadan önce STARTTLS komutunu göndermek için
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(_smtpUserName, _smtpPassword);
                        client.TargetName = _smtpHost;
                        client.Port = _smtpPort;
                        client.Send(message);
                    }
                }
                
            }
            catch (Exception ex)
            {
                // Hata durumunda istenilen işlemler yapılabilir
                throw ex;
            }
        }
    }
}