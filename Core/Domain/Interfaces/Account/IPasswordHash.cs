public interface IPasswordHasher
{
    string EncryptPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}