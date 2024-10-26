using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Model;

namespace AikoLearning.Core.Domain.Interfaces;

public interface IArticleRepository : IBaseRepository<Article, Articles>
{
    Task<Article> GetByName(string name);
    Task<IEnumerable<Article>> GetByCategory(int categoryId);
    Task<IEnumerable<Article>> GetByUser(int userId);
    Task<IEnumerable<Articles>> GetPagedByCategories(int[] categoryIDs, int pageNumber, int pageLimit);
}
