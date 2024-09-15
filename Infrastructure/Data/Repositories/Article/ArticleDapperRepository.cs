using System.Data;
using AikoLearning.Core.Application.Base;
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

    #region GetAll
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
    #endregion

    #region GetById
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
    #endregion

    #region GetByName
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
    #endregion

    #region GetByCategory
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

    #region GetPagedByCategories
    public async Task<IEnumerable<Articles>> GetPagedByCategories(int[] categoryIDs, int pageNumber, int pageLimit)
    {
        var sql = @"SELECT a.""id"", 
                           a.""name"", 
                           a.""category_id"",
                           u.""name"" AS author,
                           a.""description"",
                           a.""image_url"",
                           a.""content""
                      FROM articles AS a
                INNER JOIN users AS u ON u.id = a.user_id
                     WHERE @categoryIDs::int[] IS NULL OR a.""category_id"" = any(@categoryIDs)
                  ";

        var query = await dbConnection.QueryAsync<Articles>(sql, new {CategoryIDs = categoryIDs});
        var skip = pageNumber * pageLimit;

        return query.Skip(skip).Take(pageLimit).ToList();
    }
    #endregion

    #region GetCount
    public async Task<int> GetCount()
    {
        var articles = await GetAll();
        return articles.Count();
    }
    #endregion

    #endregion
}