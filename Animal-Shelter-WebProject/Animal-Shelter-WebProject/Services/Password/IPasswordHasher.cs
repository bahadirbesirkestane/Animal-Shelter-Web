namespace Animal_Shelter_WebProject.Services.Password
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string actualPassword, string hashedPassword);
    }
}
