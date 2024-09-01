using System.Data;
using AikoLearning.Core.Domain.Enums;
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

    #region GetAll
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
    #endregion

    #region GetById
    public async Task<Users> GetById(int id)
    {
        var query = @"SELECT u.""id"", 
                             u.""name"", 
                             u.""email"",
                             u.""is_admin""
                        FROM users AS u  
                       WHERE u.""id"" = @id                  
                    ";

        var user = await dbConnection.QueryFirstOrDefaultAsync<Users>(query, new {id = id});
        return user;
    }
    #endregion

    #region GetByEmail
    public async Task<Users> GetByEmail(string email)
    {
        var query = @"SELECT u.""id"", 
                             u.""name"", 
                             u.""email"",
                             u.""is_admin""
                        FROM users AS u  
                       WHERE u.""email"" = @email                  
                    ";
        var user = await dbConnection.QueryFirstOrDefaultAsync<Users>(query, new {email = email});
        return user;
    }
    #endregion

    #region GetRoles
    public async Task<Role[]> GetRoles(long userID)
    {
        var query = 
            @"
                SELECT DISTINCT unnest(roles) 
                  FROM users 
                 WHERE id = @userID
            ";

        var roles = await dbConnection.QueryAsync<Role>(query, new {userID = userID});
        return roles.ToArray();
    }
    #endregion
    
    #endregion
}
