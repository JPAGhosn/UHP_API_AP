using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using UHP.Infrastructure.Configurations.Settings;
using UHP.Infrastructure.Interfaces;

namespace UHP.Infrastructure.Service
{
    public class MailService : IMailService
    {
        private EmailConfiguration Configuration { get; set; }

        public MailService(EmailConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Send(string subject, string body, string toRecipient, string qrcodeImage)
        {
            var message = new MailMessage();
            var smtp = new SmtpClient();
            
            var linkedResource = new LinkedResource(qrcodeImage, "image/png");
            
            body = body.Replace("qrcodeImage", linkedResource.ContentId);
            
            var alternateView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(linkedResource);
            
            message.From = new MailAddress(Configuration.SenderAddress, Configuration.SenderDisplayName);
            message.To.Add(new MailAddress(toRecipient));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            message.AlternateViews.Add(alternateView); 
            
            smtp.Port = Configuration.Port;
            smtp.Host = Configuration.Host;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(Configuration.SenderAddress, Configuration.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}