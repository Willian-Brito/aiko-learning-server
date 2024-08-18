using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Account;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AikoLearning.Infrastructure.Security.Tokens;

public class UserTokenJWT : IUserToken
{
    #region Properties
    private readonly IConfiguration configuration;
    #endregion

    #region Constructors
    public UserTokenJWT(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    #endregion

    #region Methods

    public UserTokenDTO Generate(string email)
    {
        #region User Info
        var claims = new[]
        {
            new Claim("email", email),
            new Claim("meuvalor", "o que voce quiser"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        #endregion

        #region Private Key
        
        // gerar chave privada para assinar o token
        var secretKey = configuration["Jwt:SecretKey"];
        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        // gerar assinatura digital
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
        
        // definir o tempo de expiração
        var expiration = DateTime.Now.AddDays(1);

        // gerar o token
        var emissor = configuration["Jwt:Issuer"];
        var audiencia = configuration["Jwt:Audience"];

        var token = new JwtSecurityToken(
            issuer: emissor,
            audience:  audiencia,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );
        #endregion

        #region UserTokenDTO
        var userToken = new UserTokenDTO()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
        #endregion

        return userToken;
    }
    #endregion
}