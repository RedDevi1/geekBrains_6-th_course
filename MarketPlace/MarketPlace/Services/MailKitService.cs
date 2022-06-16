using MarketPlace.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;

namespace MarketPlace.Services
{
    public class MailKitService : IEmailService
    {
        private readonly SmtpCredentials _smtpCredentials;
        private readonly IConfiguration _config;
        public MailKitService (IOptions<SmtpCredentials> options, IConfiguration config)
        {
            _smtpCredentials = options.Value;
            _config = config;
        }
        public void SendEmail(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("MarketPlace", "asp2022gb@rodion-m.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = message
            };
            using (var client = new SmtpClient())
            {
                client.Connect(_smtpCredentials.Host, 25, false);
                client.Authenticate();
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
