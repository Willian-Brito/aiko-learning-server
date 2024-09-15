using AikoLearning.Core.Domain.Model;
using Model = AikoLearning.Infrastructure.Data.MongoDB.Models;

namespace AikoLearning.Infrastructure.Data.MongoDB.Mappings;

public static class StatsMapper
{
    public static List<Stats> ToEntities(List<Model.Stats> stats)
    {
        var entities = new List<Stats>();

        stats.ForEach(stat => 
        {
            var entity = new Stats
            (
                stat.UsersCount,
                stat.CategoriesCount,
                stat.ArticlesCount,
                stat.CreatedAt
            );

            entities.Add(entity);
        });

        return entities;
    }

    public static Stats ToEntity(Model.Stats stats)
    {
        var entity = new Stats
        (
            stats.UsersCount,
            stats.CategoriesCount,
            stats.ArticlesCount,
            stats.CreatedAt
        );

        return entity;
    }

    public static Model.Stats ToModel(Stats stats)
    {
        var model = new Model.Stats
        (
            stats.ID,
            stats.UsersCount,
            stats.CategoriesCount,
            stats.ArticlesCount,
            stats.CreatedAt
        );

        return model;
    }
}