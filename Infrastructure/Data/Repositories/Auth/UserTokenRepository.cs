using AikoLearning.Core.Domain.Account;
using AikoLearning.Core.Domain.Model;
using AikoLearning.Infrastructure.Data.Base;
using AikoLearning.Infrastructure.Data.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AikoLearning.Infrastructure.Data.Repositories;

public class UserTokenRepository : BaseRepository<UserToken, UserTokens>, IUserTokenRepository
{
    public UserTokenRepository(ApplicationDbContext context, IMapper mapper) 
        : base(context, mapper) { }

    public async Task<UserToken> GetByToken(string token)
    {
        var model = await dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Token == token);
        var userToken = mapper.Map<UserToken>(model);
        
        return userToken;
    }

    public async Task<UserToken> GetByUser(int userId)
    {
        var model = await dbSet.AsNoTracking()
            .OrderByDescending(t => t.ExpiryDate)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        var userToken = mapper.Map<UserToken>(model);
        return userToken;
    }

    public async Task DeleteAllTokensByUser(int userId)
    {
        var tokens = await dbSet.AsNoTracking()
            .Where(c => c.UserId == userId).ToListAsync();

        foreach (var token in tokens)
            await Delete(token.ID);
    }

    
}
