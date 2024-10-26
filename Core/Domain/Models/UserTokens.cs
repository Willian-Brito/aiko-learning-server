using System.ComponentModel.DataAnnotations.Schema;
using AikoLearning.Core.Domain.Base;

namespace AikoLearning.Core.Domain.Model;

[Table("user_tokens")]
public class UserTokens : BaseModel
{
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("access_token")]
    public string AccessToken { get; set; }

    [Column("refresh_token")]
    public string RefreshToken { get; set; }

    [Column("access_token_expiration")]
    public DateTime AccessTokenExpiration { get; set; }

    [Column("refresh_token_expiration")]
    public DateTime RefreshTokenExpiration { get; set; }

    [ForeignKey(nameof(UserId))]
    public Users User { get; set; }

    public UserTokens() { }

    public UserTokens(int userId, string accessToken, string refreshToken, DateTime accessTokenExpiration, DateTime refreshTokenExpiration)
    { 
        UserId = userId;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        AccessTokenExpiration = accessTokenExpiration;
        RefreshTokenExpiration = refreshTokenExpiration;
    }
}
