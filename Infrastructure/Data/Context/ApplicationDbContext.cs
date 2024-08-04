using AikoLearning.Core.Domain.Entities;
using AikoLearning.Infrastructure.Data.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AikoLearning.Infrastructure.Data.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    #region Entities
    public DbSet<Category> Categories { get; set; }
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
    #endregion
}
