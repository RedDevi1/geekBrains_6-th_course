using MarketPlace.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;

namespace MarketPlace.Services
{
    public class MailKitService : IEmailService, IDisposable
    {
        private readonly SmtpClient _client;
        private readonly SmtpCredentials _smtpCredentials;
        public MailKitService (IOptions<SmtpCredentials> options)
        {
            _smtpCredentials = options.Value;
            _client = new SmtpClient();
        } 

        public void Dispose()
        {
            if (_client.IsConnected)
                _client.Disconnect(true);
            _client.Dispose();
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("MarketPlace", "asp2022gb@rodion-m.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = message
            };

            if (!_client.IsConnected)
                await _client.ConnectAsync(_smtpCredentials.Host, 25, false);
            if (!_client.IsAuthenticated)
                await _client.AuthenticateAsync(_smtpCredentials.UserName, _smtpCredentials.Password);
            await _client.SendAsync(emailMessage);
        }
    }
}
