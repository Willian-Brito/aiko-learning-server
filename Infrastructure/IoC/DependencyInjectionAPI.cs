using System.Data;
using AikoLearning.Core.Application.Interfaces;
using AikoLearning.Core.Application.Mappings;
using AikoLearning.Core.Application.Services;
using AikoLearning.Core.Domain.Account;
using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Infrastructure.Data.Base;
using AikoLearning.Infrastructure.Data.Context;
using AikoLearning.Infrastructure.Data.Repositories;
using AikoLearning.Infrastructure.Security.Hashs;
using AikoLearning.Infrastructure.Security.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace AikoLearning.Infrastructure.IoC;

public static class DependencyInjectionAPI
{
    #region AddInfrastructureAPI
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services, IConfiguration configuration)
    {
        #region Database

        var postgresConnection = configuration.GetConnectionString("Postgres");

        services.AddDbContext<ApplicationDbContext>(options =>
         options.UseNpgsql(postgresConnection, 
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddSingleton<IDbConnection>(provider => 
        {
            var connection = new NpgsqlConnection(postgresConnection);
            connection.Open();
            return connection;
        });

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        #endregion

        #region Identity
        // services.AddIdentity<ApplicationUser, IdentityRole>()
        //         .AddEntityFrameworkStores<ApplicationDbContext>()
        //         .AddDefaultTokenProviders();
        #endregion

        #region Unity Of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        #region Services
        services.AddScoped<ICategoryService, CategoryService>();
        // services.AddScoped<IArticleService, ArticleService>();
        #endregion

        #region Repository
        
        #region Dapper
        services.AddScoped<ICategoryDapperRepository, CategoryDapperRepository>();
        services.AddScoped<IArticleDapperRepository, ArticleDapperRepository>();
        services.AddScoped<IUserDapperRepository, UserDapperRepository>();
        #endregion

        #region Entity Framework
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        #endregion

        #endregion

        #region Auth
        // services.AddScoped<IAuthenticate, AuthenticateService>();
        // services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        services.AddScoped<IUserToken, UserTokenJWT>();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        #endregion

        #region Auto Mapper
        services.AddAutoMapper(typeof(DomainToDtoMapping));
        #endregion

        #region MediatoR
        var handlers = AppDomain.CurrentDomain.Load("Application");
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(handlers));
        #endregion

        return services;
    }
    #endregion

    #region ApplyMigrations
    public static async Task ApplyMigrations(this IApplicationBuilder app)
    {
        using (var serviceScope = app. ApplicationServices.CreateScope())
        {            
            var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();   
            context.Database.Migrate();
        }
    }
    #endregion
}