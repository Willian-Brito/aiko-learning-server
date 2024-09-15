using System.Data;
using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Core.Domain.Model;
using AikoLearning.Infrastructure.Data.MongoDB.Settings;
using Microsoft.Extensions.Options;
using Model = AikoLearning.Infrastructure.Data.MongoDB.Models;
using MongoDB.Driver;
using AikoLearning.Infrastructure.Data.MongoDB.Mappings;

namespace AikoLearning.Infrastructure.Data.Repositories;

public class StatRepository : IStatRepository
{
    #region Properties
    private readonly IMongoCollection<Model.Stats> collection;
    #endregion

    #region Constructor
    public StatRepository(IOptions<MongoDBSettings> settings)
    {
        var mongoClient = new MongoClient(settings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        collection = database.GetCollection<Model.Stats>("stats");
    }
    #endregion

    #region Methods
  
    #region GetLast
    public async Task<Stats> GetLast()
    {
        var models = await collection.Find(_ => true).ToListAsync();
        var entities = StatsMapper.ToEntities(models);

        return entities.OrderByDescending(s => s.CreatedAt).FirstOrDefault();
    }
    #endregion

    #region GetAll
    public async Task<List<Stats>> GetAll()
    {        
        var models = await collection.Find(_ => true).ToListAsync();
        var entities = StatsMapper.ToEntities(models);

        return entities;
    }
    #endregion

    #region Create
    public async Task<Stats> Create(Stats entity)
    {
        var model = StatsMapper.ToModel(entity);
        await collection.InsertOneAsync(model);
        return entity;
    }
    #endregion

    #endregion
}