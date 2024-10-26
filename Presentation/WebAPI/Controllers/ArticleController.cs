using AikoLearning.Core.Application.Articles.Queries;
using AikoLearning.Core.Application.Categories.Commands;
using AikoLearning.Core.Domain.Enums;
using AikoLearning.Core.Domain.Exceptions;
using AikoLearning.Presentation.WebAPI.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AikoLearning.Presentation.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ArticleController : CustomController
{
    #region Properties
    private readonly IMediator mediator;
    #endregion

    #region Constructors
    public ArticleController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    #endregion

    #region Actions

    #region Commands

    #region Create
    [HttpPost]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<ActionResult> Create(CreateArticleCommand command)
    {
        var dto = await mediator.Send(command);
        var response = BaseResponseAPI.Create(dto);

        return CustomResponse(response);
    }
    #endregion

    #region Update
    [HttpPut("{id:int}")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<ActionResult> Update(int id, UpdateArticleCommand command)
    {
        if(id != command.ID)
            throw new BadRequestException("O ID do parâmetro da URL não corresponde ao ID da categoria do corpo da requisição");

        var dto = await mediator.Send(command);
        var response = BaseResponseAPI.Create(dto);

        return CustomResponse(response);
    }
    #endregion

    #region Delete
    [HttpDelete("{id:int}")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteArticleCommand{ ID = id };
        await mediator.Send(command);
        
        var response = BaseResponseAPI.Create("Artigo removido com sucesso!");
        return CustomResponse(response);
    }
    #endregion

    #endregion

    #region Queries

    #region GetPaged
    [HttpGet("paged")]    
    public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageLimit = 10)
    {
        var query = new GetPagedArticlesQuery(pageNumber, pageLimit);
        var paged = await mediator.Send(query);

        if (paged == null)
            throw new NotFoundException("Não foi possível encontrar os artigos");         

        var response = BaseResponseAPI.Create(paged);
        return CustomResponse(response);
    }
    #endregion

    #region GetPagedByCategory
    [HttpGet("category/{id:int}")]
    public async Task<ActionResult> GetPagedByCategory(
        [FromRoute] int id,
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageLimit = 10
    )
    {
        var query = new GetPagedArticlesByCategoryQuery(id, pageNumber, pageLimit);
        var dto = await mediator.Send(query);

        if(dto is null)
            throw new NotFoundException("Não foi possível encontrar os artigos"); 

        var response = BaseResponseAPI.Create(dto);
        return CustomResponse(response);
    }
    #endregion

    #region GetAll
    [HttpGet]    
    public async Task<ActionResult> GetAll()
    {
        var query = new GetAllArticlesQuery();
        var dtos  = await mediator.Send(query);

        if (dtos == null)
            throw new NotFoundException("Não foi possível encontrar os artigos"); 

        var response = BaseResponseAPI.Create(dtos);
        return CustomResponse(response);
    }
    #endregion

    #region GetById
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id)
    {
        var query = new GetArticleByIdQuery { ID = id };
        var dto = await mediator.Send(query);

        if(dto is null)
            throw new NotFoundException("Artigo não existe!");

        var response = BaseResponseAPI.Create(dto);
        return CustomResponse(response);
    }
    #endregion

    #region GetByName

    [HttpGet("name/{name}")]
    public async Task<ActionResult> GetByName(string name)
    {
        var query = new GetArticleByNameQuery { Name = name };
        var dto  = await mediator.Send(query);

        if(dto is null)
            throw new NotFoundException("Artigo não existe!");

        var response = BaseResponseAPI.Create(dto);
        return CustomResponse(response);
    }
    #endregion

    #endregion

    #endregion
}
