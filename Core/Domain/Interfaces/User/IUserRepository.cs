using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Enums;
using AikoLearning.Core.Domain.Model;

namespace AikoLearning.Core.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User, Users>
{
    Task<User> GetByEmail(string email);
    Task<List<Role>> GetRoles(long userID);
    Task<bool> IsAdmin(int userID);
}