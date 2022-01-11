using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Online_Store.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Online_Store Computer Components", "heroes51635@mail.ru"));
            emailMessage.To.Add(new MailboxAddress("heroes51635@mail.ru", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 25, false);
                await client.AuthenticateAsync("heroes51635@mail.ru", "Asterisk_sk");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            } 
        }

    }
}
