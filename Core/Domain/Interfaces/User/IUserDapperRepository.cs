using AikoLearning.Core.Domain.Model;

namespace AikoLearning.Core.Domain.Interfaces;

public interface IUserDapperRepository
{
    Task<IEnumerable<Users>> GetAll();
    Task<Users> GetById(int id);
}
