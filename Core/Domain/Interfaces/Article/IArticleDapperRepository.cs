using AikoLearning.Core.Domain.Model;

namespace AikoLearning.Core.Domain.Interfaces;

public interface IArticleDapperRepository 
{
    Task<IEnumerable<Articles>> GetAll();
    Task<Articles> GetById(int id);
    Task<Articles> GetByName(string name);
}