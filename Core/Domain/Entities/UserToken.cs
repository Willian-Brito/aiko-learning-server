using AikoLearning.Core.Domain.Base;
using AikoLearning.Core.Domain.Entities;

namespace AikoLearning.Core.Domain.Account;

public sealed class UserToken : BaseEntity
{
    public int UserId { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
    public User? User { get; set; }

    private UserToken() { }

    public static UserToken Create (
        int userId, 
        string accessToken, 
        string refreshToken, 
        DateTime accessTokenExpiration,
        DateTime refreshTokenExpiration
    ) 
    { 
        var userToken = new UserToken
        {
            UserId = userId,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiration = accessTokenExpiration,
            RefreshTokenExpiration = refreshTokenExpiration
        };

        return userToken;
    }
}
