using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.API.Services
{
    public class MailHelperService
    {
        private readonly string _noReplyName;
        private readonly string _noReplyEmail;
        private readonly string _smtpHost;
        private readonly int _smtpPort;

        public MailHelperService(IConfiguration configuration)
        {
            // _noReplyName = configuration["Smtp:NoReply:Name"]!;

            _noReplyName = configuration.GetValue<string>("Smtp:NoReply:Name")!;
            _noReplyEmail = configuration.GetValue<string>("Smtp:NoReply:Email")!;
            _smtpHost = configuration.GetValue<string>("Smtp:Host")!;
            _smtpPort = configuration.GetValue<int>("Smtp:Port")!;
        }

        private SmtpClient GetSmtpClient()
        {
            SmtpClient client = new SmtpClient();
            client.Connect(_smtpHost, _smtpPort);
            //Si necessaire -> client.Authenticate(...)

            return client;
        }

        public void SendWelcome(User user)
        {
            string username = user.FirstName + " " + user.LastName;

            // Création du mail
            MimeMessage email = new MimeMessage();
            email.From.Add(new MailboxAddress(_noReplyName, _noReplyEmail));
            email.To.Add(new MailboxAddress(username, user.Email));
            email.Subject = "Bievenue sur notre superbe démo O(∩_∩)O";
            email.Body = new TextPart(TextFormat.Plain)
            {
                Text = "Ceci est une démo pour les Dev .Net de technifutur ! \n\n" +
                       "(✿◡‿◡) \n\n\n" +
                       "Cordalement Zaza."
            };

            using var client = GetSmtpClient();
            client.Send(email);
            client.Disconnect(true);
        }

       
    }
}
