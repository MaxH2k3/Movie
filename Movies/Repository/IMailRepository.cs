using MimeKit;
using Movies.Business.users;
using Movies.Models;

namespace Movies.Repository;

public interface IMailRepository
{
    Task<bool> SendMail(MimeMessage mimeMessage);
    MimeMessage CreateMail(Mail mail, UserMail userMail);
    MimeMessage CreateMailWithAttachment(Mail mail, UserMail userMail, string attachmentFilePath);
    string TransferData(Dictionary<string, object> models, string htmlFile);
}
