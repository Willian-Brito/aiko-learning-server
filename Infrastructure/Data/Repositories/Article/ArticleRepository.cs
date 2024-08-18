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
        var model = dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
        var article = mapper.Map<Article>(model);
        return article;
    }
}
