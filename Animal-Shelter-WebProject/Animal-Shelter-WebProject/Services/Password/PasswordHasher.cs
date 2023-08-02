using System.Web.Helpers;

namespace Animal_Shelter_WebProject.Services.Password
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }
        public bool VerifyPassword(string actualPassword, string hashedPassword)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, actualPassword);
        }
    }
}
