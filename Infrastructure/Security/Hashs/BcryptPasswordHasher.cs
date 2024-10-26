namespace AikoLearning.Infrastructure.Security.Hashs;

public class BcryptPasswordHasher : IPasswordHasher
{
    public string EncryptPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        if(string.IsNullOrWhiteSpace(password))
            return false;

        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
