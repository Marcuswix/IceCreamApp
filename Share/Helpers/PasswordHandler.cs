using System.Security.Cryptography;
using System.Text;

namespace Share.Helpers
{
    public class PasswordHandler
    {
        public static string GenerateSecurePassword(string password)
        {
            using var hmac = new HMACSHA256();
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
    }
}
