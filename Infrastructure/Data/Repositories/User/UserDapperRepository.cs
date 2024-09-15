using System.Data;
using System.Data.Common;
using AikoLearning.Core.Domain.Account;
using AikoLearning.Core.Domain.Enums;
using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Core.Domain.Model;
using Dapper;

namespace AikoLearning.Infrastructure.Data.Repositories;

public class UserDapperRepository : IUserDapperRepository
{
    #region Properties
    private readonly IRoleService roleService;
    private readonly IDbConnection dbConnection;
    #endregion

    #region Constructor
    public UserDapperRepository(IDbConnection dbConnection, IRoleService roleService)
    {
        this.dbConnection = dbConnection;
        this.roleService = roleService;
    }
    #endregion

    #region Methods

    #region GetAll
    public async Task<IEnumerable<Users>> GetAll()
    {
        var query = @"SELECT u.""id"", 
                             u.""name"", 
                             u.""email"",
                             u.""roles""  
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
                             u.""roles""
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
                             u.""roles""
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

    #region GetCount
    public async Task<int> GetCount()
    {
        var users = await GetAll();
        return users.Count();
    }
    #endregion
    
    #endregion
}
