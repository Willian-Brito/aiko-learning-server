using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Infrastructure.Data.Base;
using AikoLearning.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AikoLearning.Infrastructure.Data.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Category> GetByName(string name)
    {
        return await dbSet.FirstOrDefaultAsync(c => c.Name == name);
    }
}
