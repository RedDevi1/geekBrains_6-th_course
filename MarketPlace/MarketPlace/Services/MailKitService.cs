using MarketPlace.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace MarketPlace.Services
{
    public class MailKitService : IEmailService
    {
        public void SendEmail(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("MarketPlace", "nickita-piter@rambler.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = message
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.beget.com", 25, false);
                client.Authenticate("asp2022gb@rodion-m.ru", "3drtLSa1");
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
