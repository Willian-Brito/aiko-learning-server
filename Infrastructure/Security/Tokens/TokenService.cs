using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AikoLearning.Core.Application.Auth.Interfaces;
using AikoLearning.Core.Application.DTOs;
using AikoLearning.Core.Domain.Account;
using AikoLearning.Core.Domain.Entities;
using AikoLearning.Infrastructure.Security.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AikoLearning.Infrastructure.Security.Tokens;

public class TokenService : ITokenService
{
    #region Properties
    private readonly IUserTokenRepository userTokenRepository;
    #endregion

    #region Constructors
    public TokenService(IUserTokenRepository userTokenRepository) 
    { 
        this.userTokenRepository = userTokenRepository;
    }
    #endregion

    #region Methods

    #region Generate
    public async Task<UserTokenDTO> Generate(User user, IEnumerable<Claim>? claims = null)
    {
        #region Claims

        var claimsIdentity = GetClaims(user, claims);
        #endregion

        #region Token
                    
        var tokenHandler = new JwtSecurityTokenHandler();
        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.SECRET_KEY));
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
        var notBefore = DateTime.Now;
        var accessTokenExpiration = DateTime.Now.AddSeconds(Settings.ACCESS_TOKEN_EXPIRATION);
        var refreshTokenExpiration = DateTime.Now.AddSeconds(Settings.REFRESH_TOKEN_EXPIRATION);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            NotBefore = notBefore,
            Expires = accessTokenExpiration,
            SigningCredentials = credentials,
            Audience = Settings.AUDIENCE,
            Issuer = Settings.ISSUER
        };

        var token = tokenHandler.CreateToken(tokenDescriptor); 
        var refreshToken = RefreshToken();
        #endregion

        #region UserTokenDTO

        var dto = new UserTokenDTO()
        {
            AccessToken = tokenHandler.WriteToken(token),
            RefreshToken = refreshToken,
            AccessTokenExpiration = accessTokenExpiration,
            RefreshTokenExpiration = refreshTokenExpiration
        };
        #endregion

        #region Save
        var userToken = UserToken.Create
        (
            user.ID, 
            dto.AccessToken, 
            refreshToken, 
            accessTokenExpiration, 
            refreshTokenExpiration
        );
        
        await userTokenRepository.DeleteAllTokensByUser(user.ID);
        await userTokenRepository.Insert(userToken);
        #endregion

        return dto;
    }
    #endregion

    #region GetClaimsFromExpiredToken
    public ClaimsPrincipal GetClaimsFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Settings.ISSUER,
            ValidAudience = Settings.AUDIENCE,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.SECRET_KEY))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var claims =  tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if(securityToken is not JwtSecurityToken jwtSecurityToken ||
          !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, 
            StringComparison.InvariantCultureIgnoreCase)
        )
        {
            throw new SecurityTokenException("Token inválido!");
        }

        return claims;
    }
    #endregion

    #region IsTokenExpired
    public async Task<bool> IsTokenExpired(string token)
    {
        var userToken = await userTokenRepository.GetByToken(token);
        return userToken.AccessTokenExpiration > DateTime.Now;
    }
    #endregion
    
    #region GetClaims
    private ClaimsIdentity GetClaims(User user, IEnumerable<Claim> claims)
    {
        if(claims is not null)
            return new ClaimsIdentity(claims);

        var claimsIdentity = new ClaimsIdentity
        (
            new[]
            {
                new Claim(Settings.USER_ID_KEY, user.ID.ToString()),
                new Claim(Settings.EMAIL_KEY, user.Email.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            }
        );

        user.Roles.ToList().ForEach(r =>
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, r.ToString()))
            );

        return claimsIdentity;
    }
    #endregion

    #region RefreshToken
    private string RefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        var Base64 = Convert.ToBase64String(randomNumber);

        return Base64;
    }
    #endregion

    #endregion
}