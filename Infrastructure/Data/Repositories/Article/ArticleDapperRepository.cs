using System.Data;
using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Core.Domain.Model;
using Dapper;

namespace AikoLearning.Infrastructure.Data.Repositories;

public class ArticleDapperRepository : IArticleDapperRepository
{
    #region Properties
    private readonly IDbConnection dbConnection;
    #endregion

    #region Constructor
    public ArticleDapperRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;    
    }
    #endregion
    
    #region Methods
    public async Task<IEnumerable<Articles>> GetAll()
    {
        var query = @"SELECT a.""id"", 
                             a.""name"", 
                             a.""category_id"",
                             a.""user_id"",
                             a.""description"",
                             a.""image_url"",
                             a.""content""
                        FROM articles AS a                    
                    ";
        return await dbConnection.QueryAsync<Articles>(query);
    }

    public async Task<Articles> GetById(int id)
    {
        var query = @"SELECT a.""id"", 
                             a.""name"", 
                             a.""category_id"",
                             a.""user_id"",
                             a.""description"",
                             a.""image_url"",
                             a.""content""
                        FROM articles AS a 
                       where a.""id"" = @id
                    ";
        return await dbConnection.QueryFirstOrDefaultAsync<Articles>(query, param: new {id = id});
    }

    public async Task<Articles> GetByName(string name)
    {
        var query = @"SELECT a.""id"", 
                             a.""name"", 
                             a.""category_id"",
                             a.""user_id"",
                             a.""description"",
                             a.""image_url"",
                             a.""content""
                        FROM articles AS a 
                       WHERE a.""name"" = @name
                    ";
        return await dbConnection.QueryFirstOrDefaultAsync<Articles>(query, param: new {name = name});
    }

    public async Task<IEnumerable<Articles>> GetByCategory(int categoryId)
    {
        var query = @"SELECT a.""id"", 
                             a.""name"", 
                             a.""category_id"",
                             a.""user_id"",
                             a.""description"",
                             a.""image_url"",
                             a.""content""
                        FROM articles AS a
                       WHERE a.""category_id"" = @categoryId
                    ";
        var articles = 
            await dbConnection.QueryAsync<Articles>(query, param: new {categoryId = categoryId});
                
        return articles;
    }
    #endregion

}