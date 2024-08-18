using AikoLearning.Core.Application.Users.Commands;
using AikoLearning.Core.Application.Users.Queries;
using AikoLearning.Presentation.WebAPI.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AikoLearning.Presentation.WebAPI.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UserController : CustomController
{
    #region Properties
    private readonly IMediator mediator;
    #endregion

    #region Constructor
    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    #endregion

    #region Actions

    #region Commands

    [AllowAnonymous]
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

    [HttpPost("register")]
    [AllowAnonymous]
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

    #region Queries

    #region GetAll
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var query = new GetAllUsersQuery();
            var dtos  = await mediator.Send(query);

            if (dtos == null)
                throw new Exception("Não foi possível encontrar os usuários"); 

            var response = BaseResponseAPI.Create(dtos);
            return CustomResponse(response);
        }
        catch(Exception ex)
        {
            return CustomResponseException(ex);
        }
    }
    #endregion

    #region GetById
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id)
    {
        try
        {
            var query = new GetUserByIdQuery { ID = id };
            var dto = await mediator.Send(query);

            if(dto == null)
                throw new Exception("Usuário não existe!");

            var response = BaseResponseAPI.Create(dto);
            return CustomResponse(response);
        }
        catch(Exception ex)
        {
            return CustomResponseException(ex);
        }
    }
    #endregion

    #endregion

    #endregion
}