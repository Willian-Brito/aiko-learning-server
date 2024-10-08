using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Core.Domain.Model;
using AikoLearning.Infrastructure.Data.Base;
using AikoLearning.Infrastructure.Data.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AikoLearning.Infrastructure.Data.Repositories;

public class ArticleRepository : BaseRepository<Article, Articles>, IArticleRepository
{
    public ArticleRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<Article> GetByName(string name)
    {
        var model = await dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
        var article = mapper.Map<Article>(model);
        return article;
    }

    public async Task<IEnumerable<Article>> GetByCategory(int categoryId)
    {
        var models = await dbSet.AsNoTracking().ToListAsync();
        var articlesModel = models.Where(c => c.CategoryId == categoryId);
        var articles = mapper.Map<IEnumerable<Article>>(articlesModel).ToList();
        return articles;
    }

    public async Task<IEnumerable<Article>> GetByUser(int userId)
    {
        var models = await dbSet.AsNoTracking().ToListAsync();
        var articlesModel = models.Where(c => c.UserId == userId);
        var articles = mapper.Map<IEnumerable<Article>>(articlesModel).ToList();
        return articles;
    }
}
