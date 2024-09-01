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
        try
        {
            var dto = await mediator.Send(command);
            var response = BaseResponseAPI.Create(dto);

            return CustomResponse(response);
        }
        catch (Exception ex)
        {
            return CustomResponseException(ex);
        }
    }
    #endregion

    #region Register

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        try
        {
            var newUser = await mediator.Send(command);
            var response = BaseResponseAPI.Create(newUser);

            return CustomResponse(response);
        }
        catch (Exception ex)
        {
            return CustomResponseException(ex);
        }
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
        try
        {
            var dto = await mediator.Send(command);
            var response = BaseResponseAPI.Create(dto);

            return CustomResponse(response);
        }
        catch (Exception ex)
        {
            return CustomResponseException(ex);
        }
    }
    #endregion

    #endregion
}