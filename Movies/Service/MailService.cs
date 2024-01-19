using MailKit.Security;
using MimeKit;
using Movies.Business.users;
using Movies.Configuration;
using Movies.Models;
using Movies.Repository;

namespace Movies.Service
{
    public class MailService : IMailRepository
    {
        private readonly GmailConfig _gmailConfig;

        public MailService(GmailConfig gmailConfig)
        {
            _gmailConfig = gmailConfig;
        }

        public MailService()
        {
            _gmailConfig = new GmailConfig();
        }

        public MimeMessage CreateMail(Mail mail, UserMail userMail)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_gmailConfig.GmailSetting.DisplayName, _gmailConfig.GmailSetting.Mail);
            email.From.Add(new MailboxAddress(_gmailConfig.GmailSetting.DisplayName, _gmailConfig.GmailSetting.Mail));
            
            email.To.Add(new MailboxAddress(mail.To, mail.To));
            email.Subject = mail.Subject;
            
            var builder = new BodyBuilder();
            //builder.HtmlBody = mail.Body;
            var htmlPart = ReadFile(mail.Body);

            //transfer data
            htmlPart = TransferData(new Dictionary<string, object>
            {
                {"username", userMail.UserName},
                {"userId", userMail.UserId},
                {"token", userMail.Token}
            }, htmlPart);

            builder.HtmlBody = htmlPart.ToString();
            email.Body = builder.ToMessageBody();

            return email;
        }

        public MimeMessage CreateMailWithAttachment(Mail mail, UserMail userMail, string attachmentFilePath)
        {
            var email = CreateMail(mail, userMail);

            var builder = new BodyBuilder();

            //attachment file
            var attachment = new MimePart("application", "octet-stream")
            {
                Content = new MimeContent(File.OpenRead(attachmentFilePath)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(attachmentFilePath)
            };

            builder.Attachments.Add(attachment);
            
            email.Body = builder.ToMessageBody();

            return email;
        }

        public async Task<bool> SendMail(MimeMessage mimeMessage)
        {
            try
            {
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                await smtp.ConnectAsync(_gmailConfig.GmailSetting.SmtpServer, _gmailConfig.GmailSetting.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_gmailConfig.GmailSetting.Mail, _gmailConfig.GmailSetting.Password);
                await smtp.SendAsync(mimeMessage);

                await smtp.DisconnectAsync(true);
            } catch (System.Exception)
            {
                Console.WriteLine("Error!");
                return false;
            }

            return true;
        }

        public string ReadFile(string path)
        {
            string htmlContent = string.Empty;
            using (StreamReader reader = File.OpenText(path))
            {
                htmlContent = reader.ReadToEnd();
            }

            return htmlContent;
        }

        public string TransferData(Dictionary<string, object> models, string htmlFile)
        {
            foreach (var item in models)
            {
                htmlFile = htmlFile.Replace("{" + item.Key + "}", item.Value.ToString());
            }
            return htmlFile;
        }

    }
}
