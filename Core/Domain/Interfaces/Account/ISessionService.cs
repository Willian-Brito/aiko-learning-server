using AikoLearning.Core.Domain.Entities;

namespace AikoLearning.Core.Domain.Interfaces;

public interface ISessionService
{
    int GetCurrentUserId();
    Task<User> GetCurrentUser();
}
