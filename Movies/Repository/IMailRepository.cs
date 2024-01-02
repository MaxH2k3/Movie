using MimeKit;
using Movies.Models;

namespace Movies.Repository
{
    public interface IMailRepository
    {
        Task<string> SendMail(MimeMessage mimeMessage);
        MimeMessage CreateMail(Mail mail); 
    }
}
