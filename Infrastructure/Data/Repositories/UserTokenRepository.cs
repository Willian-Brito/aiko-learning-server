using System.Data;
using AikoLearning.Core.Domain.Account;
using AikoLearning.Core.Domain.Model;
using AikoLearning.Infrastructure.Data.Base;
using AikoLearning.Infrastructure.Data.Context;
using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace AikoLearning.Infrastructure.Data.Repositories;

public class UserTokenRepository : BaseRepository<UserToken, UserTokens>, IUserTokenRepository
{
    public UserTokenRepository(ApplicationDbContext context, IMapper mapper, IDbConnection dbConnection) 
        : base(context, mapper, dbConnection) { }

    public async Task<UserToken> GetByToken(string token)
    {

        var query = @"SELECT ut.""id"", 
                             ut.""user_id"", 
                             ut.""access_token"",
                             ut.""refresh_token"",
                             ut.""access_token_expiration"",
                             ut.""refresh_token_expiration""
                        FROM user_tokens AS ut
                       WHERE ut.""access_token"" = @token
                    ";
        var userToken = await dbConnection
            .QueryFirstOrDefaultAsync<UserToken>(query, param: new {token = token});
        
        return userToken;
    }

    public async Task<UserToken> GetByUser(int userId)
    {
        var query = @"SELECT ut.""id"", 
                             ut.""user_id"", 
                             ut.""access_token"",
                             ut.""refresh_token"",
                             ut.""access_token_expiration"",
                             ut.""refresh_token_expiration""
                        FROM user_tokens AS ut
                       WHERE ut.""user_id"" = @userId
                    ORDER BY access_token_expiration DESC
                    ";
        var userToken = await dbConnection
            .QueryFirstOrDefaultAsync<UserToken>(query, param: new {userId = userId});

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
