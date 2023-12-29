using System.Security.Cryptography;
using System.Text;

namespace Movies.Security;

public interface IAuthentication
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    string CreateRandomToken();
}
