using System.Data;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Interfaces;
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
    public async Task<IEnumerable<Category>> GetAll()
    {
        var query = @"SELECT c.""ID"", 
                             c.""Name"", 
                             c.""ParentId""  
                        FROM category AS c 
                       WHERE c.""DeletedAt"" IS NULL
                    ";
        return await dbConnection.QueryAsync<Category>(query);
    }

    public async Task<Category> GetById(int id)
    {
        var query = @"SELECT c.""ID"", 
                             c.""Name"", 
                             c.""ParentId""  
                        FROM category AS c 
                       WHERE c.""ID"" = @id 
                         AND c.""DeletedAt"" IS NULL
                    ";

        return await dbConnection.QueryFirstOrDefaultAsync<Category>(query, param: new {id = id});
    }

    public async Task<Category> GetByName(string name)
    {
        var query = @"SELECT c.""ID"", 
                             c.""Name"", 
                             c.""ParentId""  
                        FROM category AS c 
                       WHERE c.""Name"" = @name 
                         AND c.""DeletedAt"" IS NULL
                    ";

        return await dbConnection.QueryFirstOrDefaultAsync<Category>(query, param: new {name = name});
    }
    #endregion
}