using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Entities;

namespace AikoLearning.Core.Domain.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<Category> GetByName(string name);
}
