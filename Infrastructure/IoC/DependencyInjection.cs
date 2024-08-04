using AikoLearning.Core.Application.Interfaces;
using AikoLearning.Core.Application.Mappings;
using AikoLearning.Core.Application.Services;
using AikoLearning.Core.Domain.Account;
using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Infrastructure.Data.Context;
using AikoLearning.Infrastructure.Data.Repositories;
using AikoLearning.Infrastructure.Data.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AikoLearning.Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        #region Database
        services.AddDbContext<ApplicationDbContext>(options =>
         options.UseNpgsql(configuration.GetConnectionString("Postgres"), 
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        #endregion

        #region Identity
        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        #endregion

        #region Redirecionamento Login
        services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Account/Login");
        #endregion

        #region Services
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        // services.AddScoped<IArticleRepository, ArticleRepository>();

        services.AddScoped<ICategoryService, CategoryService>();
        // services.AddScoped<IArticleService, ArticleService>();
        #endregion

        #region Auth
        services.AddScoped<IAuthenticate, AuthenticateService>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        #endregion

        #region Auto Mapper
        services.AddAutoMapper(typeof(DomainToDtoMapping));
        #endregion

        return services;
    }
} 