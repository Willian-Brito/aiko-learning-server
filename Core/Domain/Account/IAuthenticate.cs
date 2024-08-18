using AikoLearning.Core.Domain.Entities;

namespace AikoLearning.Core.Domain.Account;

public interface IAuthenticate
{
    Task<bool> Authenticate(string email, string password);
    Task<User> Register(string email, string password);
    Task Logout();
}
