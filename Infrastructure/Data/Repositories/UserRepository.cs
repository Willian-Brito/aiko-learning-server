using System.Data;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Enums;
using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Core.Domain.Model;
using AikoLearning.Infrastructure.Data.Base;
using AikoLearning.Infrastructure.Data.Context;
using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace AikoLearning.Infrastructure.Data.Repositories;

public class UserRepository : BaseRepository<User, Users>, IUserRepository
{
    public UserRepository(ApplicationDbContext context, IMapper mapper, IDbConnection dbConnection) 
        : base(context, mapper, dbConnection) { }

    public override async Task<IEnumerable<User>> GetAll()
    {
        var models = await dbSet.AsNoTracking().ToListAsync();
        var users = mapper.Map<IEnumerable<User>>(models);
        return users;
    }

    public override async Task<User> Get(int id)
    {
        var models = await dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.ID == id);
        var user = mapper.Map<User>(models);
        return user;
    }

    public async Task<User> GetByEmail(string email)
    {
        var model = await dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        var user = mapper.Map<User>(model);
        return user;
    }

    public async Task<List<Role>> GetRoles(long userID)
    {
        var user = await dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.ID == userID);        
        return user.Roles;
    } 
        
    // public async Task<List<Role>> GetRoles(long userID)
    // {
    //     var query = 
    //         @"
    //             SELECT DISTINCT unnest(roles) 
    //               FROM users 
    //              WHERE id = @userID
    //         ";

    //     var roles = await dbConnection.QueryAsync<Role>(query, new {userID = userID});
    //     return roles.ToList();
    // }

    public async Task<bool> IsAdmin(int userID)
    {              
        var user = await Get(userID);
        var isAdmin = user.Roles.Any(r => r is Role.Administrator);       
        return isAdmin;
    }  

    public async Task<int> GetCount()
    {
        var users = await GetAll();
        return users.Count();
    }
}
