using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Model;

namespace AikoLearning.Core.Domain.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category, Categories>
{
    Task<Category> GetByName(string name);
    Task<IEnumerable<Category>> GetSubcategories(int id);
    Task<Category> GetParent(int? parentId);
}
