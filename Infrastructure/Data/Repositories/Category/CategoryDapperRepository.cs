using System.Data;
using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Core.Domain.Model;
using Dapper;

namespace AikoLearning.Infrastructure.Data.Repositories;

public class CategoryDapperRepository : ICategoryDapperRepository
{
    #region Properties
    private readonly IDbConnection dbConnection;
    #endregion

    #region Constructor
    public CategoryDapperRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;    
    }
    #endregion

    #region Methods

    #region GetAll
    public async Task<IEnumerable<Categories>> GetAll()
    {
        var query = @"SELECT c.""id"", 
                             c.""name"", 
                             c.""parent_id""  
                        FROM categories AS c                    
                    ";
        var categories = await dbConnection.QueryAsync<Categories>(query);
        return categories.ToList();
    }
    #endregion

    #region GetById
    public async Task<Categories> GetById(int id)
    {
        var query = @"SELECT c.""id"", 
                             c.""name"", 
                             c.""parent_id""  
                        FROM categories AS c 
                       WHERE c.""id"" = @id                         
                    ";
        var category = 
            await dbConnection.QueryFirstOrDefaultAsync<Categories>(query, new {id = id});

        return category;
    }
    #endregion

    #region GetByName
    public async Task<Categories> GetByName(string name)
    {
        var query = @"SELECT c.""id"", 
                             c.""name"", 
                             c.""parent_id""  
                        FROM categories AS c 
                       WHERE c.""name"" = @name                          
                    ";
        var category = 
            await dbConnection.QueryFirstOrDefaultAsync<Categories>(query, new {name = name});

        return category;
    }
    #endregion

    #region GetSubcategories
    public async Task<IEnumerable<Categories>> GetSubcategories(int id)
    {
        var query = @"SELECT c.""id"", 
                             c.""name"", 
                             c.""parent_id""  
                        FROM categories AS c 
                       WHERE c.""parent_id"" = @id
                    ";
        var subCategories = 
            await dbConnection.QueryAsync<Categories>(query, param: new {id = id});
        
        return subCategories.ToList();
    }
    #endregion

    #region GetCategoryWithChildren
    public async Task<int[]> GetCategoryIDsWithChildren(int id)
    {
        var sql =  
            @"
                WITH RECURSIVE subcategories (id) AS 
                (
                    SELECT id 
                    FROM categories 
                    WHERE id = @id
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
    #endregion

    #region GetCount
    public async Task<int> GetCount()
    {
        var categories = await GetAll();
        return categories.Count();
    }
    #endregion
    
    #endregion
}