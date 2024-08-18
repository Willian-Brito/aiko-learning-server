using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Core.Domain.Model;
using AikoLearning.Infrastructure.Data.Base;
using AikoLearning.Infrastructure.Data.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AikoLearning.Infrastructure.Data.Repositories;

public class CategoryRepository : BaseRepository<Category, Categories>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<Category> GetByName(string name)
    {
        var model = dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
        var category = mapper.Map<Category>(model);
        return category;
    }
}
