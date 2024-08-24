using AikoLearning.Core.Domain.Model;

namespace AikoLearning.Core.Domain.Interfaces;

public interface ICategoryDapperRepository 
{
    Task<IEnumerable<Categories>> GetAll();
    Task<Categories> GetById(int id);
    Task<Categories> GetByName(string name);
    Task<IEnumerable<Categories>> GetSubcategories(int id);
}