using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Server
{
    public class AuthOptions
    {
        public const string ISSUER = "https://localhost:7116";
        public const string AUDIENCE = "https://localhost:7116";
        public const string KEY = "mysupersecret_secretkey!";   // ключ для шифрации
        public const int LIFETIME = 3;
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
