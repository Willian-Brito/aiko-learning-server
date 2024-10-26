namespace AikoLearning.Core.Application.DTOs;

public class UserTokenDTO
{
    public int ID { get; set; }
    public string Name { get; set; }    
    public string Email { get; set; }
    public string[] Roles { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}
