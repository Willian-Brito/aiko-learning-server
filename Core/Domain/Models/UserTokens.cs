using System.ComponentModel.DataAnnotations.Schema;
using AikoLearning.Core.Domain.Base;

namespace AikoLearning.Core.Domain.Model;

[Table("user_tokens")]
public class UserTokens : BaseModel
{
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("token")]
    public string Token { get; set; }

    [Column("refresh_token")]
    public string RefreshToken { get; set; }

    [Column("expiry_date")]
    public DateTime ExpiryDate { get; set; }

    [ForeignKey(nameof(UserId))]
    public Users User { get; set; }

    public UserTokens() { }

    public UserTokens(int userId, string token, string refreshToken, DateTime expireDate) 
    { 
        UserId = userId;
        Token = token;
        RefreshToken = refreshToken;
        ExpiryDate = expireDate;
    }
}
