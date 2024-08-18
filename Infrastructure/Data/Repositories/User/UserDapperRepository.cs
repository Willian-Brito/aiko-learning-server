using System.Data;
using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Core.Domain.Model;
using Dapper;

namespace AikoLearning.Infrastructure.Data.Repositories;

public class UserDapperRepository : IUserDapperRepository
{
    #region Properties
    private readonly IDbConnection dbConnection;
    #endregion

    #region Constructor
    public UserDapperRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;    
    }
    #endregion

    #region Methods
    public async Task<IEnumerable<Users>> GetAll()
    {
        var query = @"SELECT u.""id"", 
                             u.""name"", 
                             u.""email"",
                             u.""is_admin""  
                        FROM users AS u                    
                    ";
        return await dbConnection.QueryAsync<Users>(query);
    }

    public async Task<Users> GetById(int id)
    {
        var query = @"SELECT u.""id"", 
                             u.""name"", 
                             u.""email"",
                             u.""is_admin""
                        FROM users AS u  
                       WHERE u.""id"" = @id                  
                    ";
        return await dbConnection.QueryFirstOrDefaultAsync<Users>(query, param: new {id = id});
    }
    #endregion
}
