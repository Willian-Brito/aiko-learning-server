using AikoLearning.Core.Domain.Base;
using AikoLearning.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AikoLearning.Infrastructure.Data.Base;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext dbContext;    
    public DbSet<TEntity> Set<TEntity>() where TEntity : class =>
            dbContext.Set<TEntity>();    

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;        
    }

    public async Task Commit()
    {
        await dbContext.SaveChangesAsync();
    }

    public void Rollback()
    {
        dbContext.Dispose();
    }
}


