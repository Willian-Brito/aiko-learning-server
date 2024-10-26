using AikoLearning.Core.Application.Users.Commands;
using AikoLearning.Core.Application.Users.Queries;
using AikoLearning.Core.Domain.Enums;
using AikoLearning.Core.Domain.Exceptions;
using AikoLearning.Core.Domain.Exceptions;
using AikoLearning.Infrastructure.Security.Sessions;
using AikoLearning.Presentation.WebAPI.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AikoLearning.Presentation.WebAPI.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = nameof(Role.Administrator))]
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

    #region Create
    [HttpPost]
    public async Task<ActionResult> Create(CreateUserCommand command)
    {
        var dto = await mediator.Send(command);
        var response = BaseResponseAPI.Create(dto);

        return CustomResponse(response);
    }
    #endregion

    #region Update
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, UpdateUserCommand command)
    {
        if(id != command.ID)
            throw new BadRequestException("O ID do parâmetro da URL não corresponde ao ID do usuário do corpo da requisição");

        var dto = await mediator.Send(command);
        var response = BaseResponseAPI.Create(dto);

        return CustomResponse(response);
    }
    #endregion

    #region Delete
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteUserCommand{ ID = id };
        await mediator.Send(command);

        var response = BaseResponseAPI.Create("Usuário removido com sucesso!");
        return CustomResponse(response); 
    }
    #endregion

    #endregion

    #region Queries

    #region GetAll
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var query = new GetAllUsersQuery();
        var dtos  = await mediator.Send(query);

        if (dtos is null)
            throw new NotFoundException("Não foi possível encontrar os usuários"); 

        var response = BaseResponseAPI.Create(dtos);
        return CustomResponse(response);
    }
    #endregion

    #region GetById
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id)
    {
        var query = new GetUserByIdQuery { ID = id };
        var dto = await mediator.Send(query);

        if(dto is null)
            throw new NotFoundException("Usuário não existe!");

        var response = BaseResponseAPI.Create(dto);
        return CustomResponse(response);
    }
    #endregion

    #region GetAllRoles
    [HttpGet("roles")]    
    public async Task<ActionResult> GetAllRoles()
    {
        var query = new GetAllRolesQuery();
        var dtos  = await mediator.Send(query);

        if (dtos is null)
            throw new NotFoundException("Não foi possível encontrar os perfis"); 

        var response = BaseResponseAPI.Create(dtos);
        return CustomResponse(response);
    }
    #endregion

    #endregion

    #endregion
}