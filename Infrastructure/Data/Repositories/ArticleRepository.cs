using System.Data;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Core.Domain.Model;
using AikoLearning.Infrastructure.Data.Base;
using AikoLearning.Infrastructure.Data.Context;
using AutoMapper;
using Dapper;

namespace AikoLearning.Infrastructure.Data.Repositories;

public class ArticleRepository : BaseRepository<Article, Articles>, IArticleRepository
{
    public ArticleRepository(ApplicationDbContext context, IMapper mapper, IDbConnection dbConnection) 
        : base(context, mapper, dbConnection) { }

    public async Task<Article> GetByName(string name)
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
        return await dbConnection.QueryFirstOrDefaultAsync<Article>(query, param: new {name = name});
    }

    public async Task<IEnumerable<Article>> GetByCategory(int categoryId)
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
            await dbConnection.QueryAsync<Article>(query, param: new {categoryId = categoryId});
                
        return articles;
    }

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

    public async Task<IEnumerable<Article>> GetByUser(int userId)
    {
        var query = @"SELECT a.""id"", 
                             a.""name"", 
                             a.""category_id"",
                             a.""user_id"",
                             a.""description"",
                             a.""image_url"",
                             a.""content""
                        FROM articles AS a
                       WHERE a.""user_id"" = @userId
                    ";
        var articles = 
            await dbConnection.QueryAsync<Article>(query, param: new {userId = userId});
                
        return articles;
    }
}
