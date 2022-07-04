using MarketPlace.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;

namespace MarketPlace.Services
{
    public class MailKitService : IEmailService, IAsyncDisposable
    {
        private readonly SmtpClient _client;
        private readonly SmtpCredentials _smtpCredentials;
        public MailKitService (IOptions<SmtpCredentials> options)
        {
            _smtpCredentials = options.Value;
            _client = new SmtpClient();
        } 

        public async ValueTask DisposeAsync()
        {
            if (_client.IsConnected)
                await _client.DisconnectAsync(true);
            _client.Dispose();
        }

        public async Task SendEmailAsync(string email, string subject, string message, CancellationToken cancellationToken)
        {
            if (email is null) throw new ArgumentNullException(nameof(email));
            if (subject is null) throw new ArgumentNullException(nameof(subject));
            if (message is null) throw new ArgumentNullException(nameof(message));
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("MarketPlace", "asp2022gb@rodion-m.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = message
            };

            if (!_client.IsConnected)
                await _client.ConnectAsync(_smtpCredentials.Host, 25, false, cancellationToken);
            if (!_client.IsAuthenticated)
                await _client.AuthenticateAsync(_smtpCredentials.UserName, _smtpCredentials.Password, cancellationToken);
            await _client.SendAsync(emailMessage, cancellationToken);
        }
    }
}
