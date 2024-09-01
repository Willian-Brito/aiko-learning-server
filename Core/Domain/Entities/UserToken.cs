using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Entities;

namespace AikoLearning.Core.Domain.Account;

public sealed class UserToken : BaseEntity
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiryDate { get; set; }
    public User? User { get; set; }

    private UserToken() { }

    public static UserToken Create (int userId, string token, string refreshToken, DateTime expireDate) 
    { 
        var userToken = new UserToken
        {
            UserId = userId,
            Token = token,
            RefreshToken = refreshToken,
            ExpiryDate = expireDate
        };

        return userToken;
    }
}
