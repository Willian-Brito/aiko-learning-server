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

    public async Task<Categories> GetById(int id)
    {
        var query = @"SELECT c.""id"", 
                             c.""name"", 
                             c.""parent_id""  
                        FROM categories AS c 
                       WHERE c.""id"" = @id                         
                    ";

        return await dbConnection.QueryFirstOrDefaultAsync<Categories>(query, param: new {id = id});
    }

    public async Task<Categories> GetByName(string name)
    {
        var query = @"SELECT c.""id"", 
                             c.""name"", 
                             c.""parent_id""  
                        FROM categories AS c 
                       WHERE c.""name"" = @name                          
                    ";

        return await dbConnection.QueryFirstOrDefaultAsync<Categories>(query, param: new {name = name});
    }

    public async Task<IEnumerable<Categories>> GetSubcategories(int parentId)
    {
        var query = @"SELECT c.""id"", 
                             c.""name"", 
                             c.""parent_id""  
                        FROM categories AS c 
                       WHERE c.""parent_id"" = @parentId
                    ";
        var subCategories = 
            await dbConnection.QueryFirstOrDefaultAsync<IEnumerable<Categories>>(query, param: new {parentId = parentId});
        
        return subCategories.ToList();
    }
    #endregion
}