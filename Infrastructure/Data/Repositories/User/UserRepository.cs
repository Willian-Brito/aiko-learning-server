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
    public UserRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

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

    public bool IsAdmin(long userID)
    {
        var user = dbSet.AsNoTracking().FirstOrDefault(u => u.ID == userID); 
        var isAdmin = user.Roles.Any(r => r is Role.Administrator);       
        return isAdmin;
    }  

    public async Task<int> GetCount()
    {
        var users = await GetAll();
        return users.Count();
    }
}
