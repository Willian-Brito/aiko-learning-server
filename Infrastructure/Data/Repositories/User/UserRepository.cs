using AikoLearning.Core.Domain.Entities;
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
}
