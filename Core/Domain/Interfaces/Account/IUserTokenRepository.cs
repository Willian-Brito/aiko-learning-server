using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Model;

namespace AikoLearning.Core.Domain.Account;

public interface IUserTokenRepository : IBaseRepository<UserToken, UserTokens>
{
    Task<UserToken> GetByUser(int userId);
    Task<UserToken> GetByToken(string token);
    Task DeleteAllTokensByUser(int userId);
}
