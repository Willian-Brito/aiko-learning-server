using AikoLearning.Core.Domain.Enums;
using AikoLearning.Core.Domain.Model;

namespace AikoLearning.Core.Domain.Interfaces;

public interface IUserDapperRepository
{
    Task<IEnumerable<Users>> GetAll();
    Task<Users> GetById(int id);
    Task<Users> GetByEmail(string email);
    Task<Role[]> GetRoles(long userID);
}
