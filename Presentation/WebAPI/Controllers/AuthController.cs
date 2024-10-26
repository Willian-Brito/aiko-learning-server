using AikoLearning.Core.Application.Auth.Commands;
using AikoLearning.Presentation.WebAPI.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AikoLearning.Presentation.WebAPI.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AuthController : CustomController
{
    #region Properties
    private readonly IMediator mediator;    
    #endregion

    #region Constructor
    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;        
    }
    #endregion

    #region Actions

    #region Login
    [HttpPost("login")]
    public async Task<ActionResult> Login(AuthenticateUserCommand command) 
    {
        var dto = await mediator.Send(command);
        var response = BaseResponseAPI.Create(dto);

        return CustomResponse(response);
    }
    #endregion

    #region Register

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var newUser = await mediator.Send(command);
        var response = BaseResponseAPI.Create(newUser);

        return CustomResponse(response);
    }
    #endregion

    #region Logout

    // [HttpPost("logout")]
    // [Authorize]
    // public async Task<IActionResult> Logout(LogoutUserCommand command)
    // {
    //     try
    //     {
    //         var newAuth = await mediator.Send(command);
    //         var response = BaseResponseAPI.Create(newAuth);

    //         return CustomResponse(response);
    //     }
    //     catch (Exception ex)
    //     {
    //         return CustomResponseException(ex);
    //     }
    // }
    #endregion

    #region RefreshToken
    [HttpPost("refresh")]
    public async Task<ActionResult> RefreshToken(RefreshUserTokenCommand command) 
    {
        var dto = await mediator.Send(command);
        var response = BaseResponseAPI.Create(dto);

        return CustomResponse(response);
    }
    #endregion

    #region ValidateToken
    [HttpPost("validateToken")]
    public async Task<ActionResult> ValidateToken(ValidateTokenCommand command) 
    {
        var dto = await mediator.Send(command);
        var response = BaseResponseAPI.Create(dto);

        return CustomResponse(response);
    }
    #endregion

    #endregion
}