using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Model;

namespace AikoLearning.Core.Domain.Account;

public interface IAuthenticateService
{
    Task<User> Authenticate(string email, string password);
    Task<Users> Register(string name, string password, string confirmPassword, string email);
    Task Logout();
}
