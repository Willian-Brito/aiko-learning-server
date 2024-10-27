using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AikoLearning.Infrastructure.Data.Context;

public class ApplicationDbContext : DbContext
{
    #region Entities    
    // public DbSet<Category> Categories { get; set; }
    // public DbSet<User> Users { get; set; }
    // public DbSet<Article> Articles { get; set; }
    // public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
    // {
    //     return Set<TEntity>();
    // }
    
    private readonly IServiceProvider serviceProvider;
    #endregion

    #region Constructor
    public ApplicationDbContext(){ }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IServiceProvider serviceProvider) : base(options)
    { 
        this.serviceProvider = serviceProvider;
    }
    #endregion

    #region Methods
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            var connectionString = "Server=localhost;Database=AikoLearningDB;Port=5432;User Id=postgres;Password=postgres;CommandTimeout=500";

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(connectionString);
        }        
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var sessionService = serviceProvider.GetRequiredService<ISessionService>();

        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
        {
            var userID = sessionService.GetCurrentUserId().ToString();
            
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = userID;
                    entry.Entity.CreatedAt = DateTime.Now;                                     
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedBy = userID;
                    entry.Entity.UpdatedAt = DateTime.Now;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.DeletedBy = userID;
                    entry.Entity.DeletedAt = DateTime.Now;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
