using System.Data;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Core.Domain.Model;
using AikoLearning.Infrastructure.Data.Base;
using AikoLearning.Infrastructure.Data.Context;
using AutoMapper;
using Dapper;

namespace AikoLearning.Infrastructure.Data.Repositories;

public class CategoryRepository : BaseRepository<Category, Categories>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context, IMapper mapper, IDbConnection dbConnection) 
        : base(context, mapper, dbConnection) { }

    public async Task<Category> GetByName(string name)
    {
        var query = @"SELECT c.""id"", 
                             c.""name"", 
                             c.""parent_id""  
                        FROM categories AS c 
                       WHERE c.""name"" = @name 
                         AND c.deleted_at IS NULL                         
                    ";
        var category = 
            await dbConnection.QueryFirstOrDefaultAsync<Category>(query, new {name = name});

        return category;
    }

    public async Task<IEnumerable<Category>> GetSubcategories(int id)
    {
        var query = @"SELECT c.""id"", 
                             c.""name"", 
                             c.""parent_id""  
                        FROM categories AS c 
                       WHERE c.""parent_id"" = @id
                         AND c.deleted_at IS NULL
                    ";
        var subCategories = 
            await dbConnection.QueryAsync<Category>(query, param: new {id = id});
        
        return subCategories.ToList();
    }

    public async Task<int[]> GetCategoryIDsWithChildren(int id)
    {
        var sql =  
            @"
                WITH RECURSIVE subcategories (id) AS 
                (
                    SELECT id 
                      FROM categories 
                     WHERE id = @id
                       AND deleted_at IS NULL
                     UNION ALL 
                    SELECT c.id
                      FROM subcategories AS s, categories AS c 
                     WHERE c.""parent_id"" = s.id
                )
                SELECT id FROM subcategories        
            ";

        var chidrens = await dbConnection.QueryAsync<Categories>(sql, new {id = id});
        return chidrens.Select(c => c.ID).ToArray();
    }

    public async Task<Category> GetParent(int? parentId)
    {
        var query = @"SELECT c.""id"", 
                             c.""name"", 
                             c.""parent_id""  
                        FROM categories AS c 
                       WHERE c.""id"" = @parentId
                         AND c.deleted_at IS NULL
                    ";
        var parent = await dbConnection.QueryFirstOrDefaultAsync<Category>(query, new {parentId = parentId});
        return parent;
    }
}
