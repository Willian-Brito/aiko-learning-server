using AikoLearning.Core.Domain.Entities;

namespace AikoLearning.Core.Domain.Interfaces;

public interface ICategoryDapperRepository 
{
    Task<IEnumerable<Category>> GetAll();
    Task<Category> GetById(int id);
    Task<Category> GetByName(string name);
}