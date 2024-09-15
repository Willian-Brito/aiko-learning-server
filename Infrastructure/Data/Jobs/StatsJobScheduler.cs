using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Infrastructure.Data.MongoDB.Mappings;
using AikoLearning.Infrastructure.Data.MongoDB.Models;
using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;

namespace AikoLearning.Infrastructure.Data.Jobs;

public class StatsJobScheduler : IJob
{
    private readonly IServiceScopeFactory serviceScopeFactory;

    public StatsJobScheduler(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }

    public void Execute()
    {
        Task.Run(async () => await ExecuteAsync());
    }

    private async Task ExecuteAsync()
    {
        try
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var statsRepository = scope.ServiceProvider.GetRequiredService<IStatRepository>();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var categoryDapperRepository = scope.ServiceProvider.GetRequiredService<ICategoryDapperRepository>();
                var articleDapperRepository = scope.ServiceProvider.GetRequiredService<IArticleDapperRepository>();

                var lastStat = await statsRepository.GetLast();
                var usersCount = await userRepository.GetCount();
                var categoriesCount = await categoryDapperRepository.GetCount();
                var articlesCount = await articleDapperRepository.GetCount();

                var newStat = new Stats(usersCount, categoriesCount, articlesCount, DateTime.Now);
                var changeUsers = lastStat is null || lastStat.UsersCount != newStat.UsersCount;
                var changeCategories = lastStat is null || lastStat.CategoriesCount != newStat.CategoriesCount;
                var changeArticles = lastStat is null || lastStat.ArticlesCount != newStat.ArticlesCount;

                if(changeUsers || changeCategories || changeArticles)
                {
                    Console.WriteLine("[Stats] Estatísticas atualizadas!");
                    var newStatsModel = StatsMapper.ToEntity(newStat);
                    await statsRepository.Create(newStatsModel);
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Erro (Stats Job): {ex.Message}");
        }
    }
}