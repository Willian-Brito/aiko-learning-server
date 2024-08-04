using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AikoLearning.Core.Domain.Account;
using AikoLearning.Presentation.WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AikoLearning.Presentation.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    #region Properties
    private readonly IAuthenticate authentication;
    private readonly IConfiguration configuration;
    #endregion

    #region Constructor
    public TokenController(IAuthenticate authentication, IConfiguration configuration)
    {
        this.authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
        this.configuration = configuration;
    }
    #endregion

    #region Actions

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<UserTokenDTO>> Login([FromBody] LoginDTO userDTO) 
    {
        var result = await authentication.Authenticate(userDTO.Email, userDTO.Password);

        if(result)
            return GenerateToken(userDTO);
        
        ModelState.AddModelError(string.Empty, "Login Inválido!");
        return BadRequest(ModelState);
    }

    [HttpPost("CreateUser")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public async Task<ActionResult> CreateUser([FromBody] LoginDTO userDTO) 
    {
        var result = await authentication.RegisterUser(userDTO.Email, userDTO.Password);

        if(result)
            return Ok($"Usuário {userDTO.Email} criado com sucesso!");
        
        ModelState.AddModelError(string.Empty, "Login Inválido!");
        return BadRequest(ModelState);
    }

    private UserTokenDTO GenerateToken(LoginDTO userDTO)
    {
        #region User Info
        var claims = new[]
        {
            new Claim("email", userDTO.Email),
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
        var expiration = DateTime.UtcNow.AddHours(4);

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