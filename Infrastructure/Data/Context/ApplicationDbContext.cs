using Microsoft.EntityFrameworkCore;

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
    #endregion

    #region Constructor
    public ApplicationDbContext(){ }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }
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

    // public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    // {
    //     foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
    //     {
    //         PropertyValues databaseValues = null;
            
    //         switch (entry.State)
    //         {
    //             case EntityState.Added:
    //                 entry.Entity.CreatedBy = _currentUserService.SessionId;
    //                 entry.Entity.CreatedAt = _dateTimeService.UtcNow;
    //                 entry.Entity.UpdatedAt = _dateTimeService.UtcNow;
    //                 entry.Entity.Version = 1;
    //                 break;
    //             case EntityState.Modified:
    //                 databaseValues = await entry.GetDatabaseValuesAsync(cancellationToken);
    //                 if (databaseValues.TryGetValue<long>(nameof(BasePersistenceEntity.Version), out var currentVersion))
    //                 {
    //                     if (currentVersion != entry.Entity.Version)
    //                     {
    //                         throw new DbUpdateConcurrencyException(
    //                             @$"Entity '{entry.GetType().Name}' was modified to version {currentVersion} while was in local cache.\nid={Json.SerializeObject(entry.Entity)}"
    //                         );
    //                     }
    //                     entry.Entity.Version++;
    //                 }
    //                 entry.Entity.UpdatedBy = _currentUserService.SessionId;
    //                 entry.Entity.UpdatedAt = _dateTimeService.UtcNow;
    //                 break;
    //         }

    //         if (entry.Entity.DeletedAt.HasValue && entry.State is EntityState.Added or EntityState.Modified)
    //         {
    //             var currentDeletedAt = databaseValues?.GetValue<DateTime?>(nameof(BasePersistenceEntity.DeletedAt));
    //             if (!currentDeletedAt.HasValue || currentDeletedAt != entry.Entity.DeletedAt)
    //             {
    //                 entry.Entity.DeletedBy = _currentUserService.SessionId;
    //             }
    //         }
    //     }

    //     return await base.SaveChangesAsync(cancellationToken);
    // }
    #endregion
}
