using System.Data;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Core.Domain.Enums;
using AikoLearning.Core.Domain.Interfaces;
using AikoLearning.Core.Domain.Model;
using AikoLearning.Infrastructure.Data.Base;
using AikoLearning.Infrastructure.Data.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AikoLearning.Infrastructure.Data.Repositories;

public class UserRepository : BaseRepository<User, Users>, IUserRepository
{
    public UserRepository(ApplicationDbContext context, IMapper mapper, IDbConnection dbConnection) 
        : base(context, mapper, dbConnection) { }

    public override async Task<IEnumerable<User>> GetPaged(int pageNumber, int pageLimit)
    {
        pageNumber = pageNumber == 0 ? 0 : pageNumber - 1;

        var models = await dbSet
            .Where(w => w.DeletedAt == null)
            .Skip(pageNumber * pageLimit)
            .Take(pageLimit)
            .ToListAsync();

        var entities = mapper.Map<IEnumerable<User>>(models);
        return entities;
    }

    public override async Task<IEnumerable<User>> GetAll()
    {
        var models = await dbSet
            .AsNoTracking()
            .Where(w => w.DeletedAt == null)
            .ToListAsync();

        var users = mapper.Map<IEnumerable<User>>(models);
        return users;
    }

    public override async Task<User> Get(int id)
    {
        var models = await dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ID == id && c.DeletedAt == null);

        var user = mapper.Map<User>(models);
        return user;
    }

    public async Task<User> GetByEmail(string email)
    {
        var model = await dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email && c.DeletedAt == null);

        var user = mapper.Map<User>(model);
        return user;
    }

    public async Task<List<Role>> GetRoles(long userID)
    {
        var user = await dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.ID == userID && u.DeletedAt == null);

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
}
