using Common.Entity;
using Datas.Models.ViewModels;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;

namespace WebApp.Common
{
    public class MailUtils
    {
        public List<MailForgot> Ids { set; get; } = new List<MailForgot>();
        public static string SendEmail(SettingModel setting,MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(setting.EmailAccount);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = setting.EmailName +" - " + mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            var port = 0;
            int.TryParse(setting.EmailPort, out port);
            smtp.Connect(setting.EmailServer, port, SecureSocketOptions.StartTls);
            smtp.Authenticate(setting.EmailAccount, setting.EmailPass);
            var result = smtp.Send(email);
            smtp.Disconnect(true);
            return result;
        }
    }
}
