using AikoLearning.Core.Application.Articles.Queries;
using AikoLearning.Core.Application.Categories.Commands;
using AikoLearning.Core.Domain.Enums;
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
        try
        {
            var dto = await mediator.Send(command);
            var response = BaseResponseAPI.Create(dto);

            return CustomResponse(response);
        }
        catch(Exception ex)
        {
            return CustomResponseException(ex);
        }
    }
    #endregion

    #region Update
    [HttpPut("{id:int}")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<ActionResult> Update(int id, UpdateArticleCommand command)
    {
        try
        {
            if(id != command.ID)
                throw new Exception("O ID do parâmetro da URL não corresponde ao ID da categoria do corpo da requisição");

            var dto = await mediator.Send(command);
            var response = BaseResponseAPI.Create(dto);

            return CustomResponse(response);
        }
        catch(Exception ex)
        {
            return CustomResponseException(ex);
        }
    }
    #endregion

    #region Delete
    [HttpDelete("{id:int}")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var command = new DeleteArticleCommand{ ID = id };
            await mediator.Send(command);
            
            var response = BaseResponseAPI.Create("Artigo removido com sucesso!");
            return CustomResponse(response);
        }
        catch(Exception ex)
        {
            return CustomResponseException(ex);
        }
    }
    #endregion

    #endregion

    #region Queries

    #region GetPaged
    [HttpGet("paged")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageLimit = 10)
    {
        try
        {
            var query = new GetPagedArticlesQuery(pageNumber, pageLimit);
            var paged = await mediator.Send(query);

            if (paged == null)
                throw new Exception("Não foi possível encontrar os artigos");         

            var response = BaseResponseAPI.Create(paged);
            return CustomResponse(response);
        }
        catch(Exception ex)
        {
            return CustomResponseException(ex);
        }
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
        try
        {
            var query = new GetPagedArticlesByCategoryQuery(id, pageNumber, pageLimit);
            var dto = await mediator.Send(query);

            if(dto == null)
                throw new Exception("Não foi possível encontrar os artigos"); 

            var response = BaseResponseAPI.Create(dto);
            return CustomResponse(response);
        }
        catch(Exception ex)
        {
            return CustomResponseException(ex);
        }
    }
    #endregion

    #region GetAll
    [HttpGet]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var query = new GetAllArticlesQuery();
            var dtos  = await mediator.Send(query);

            if (dtos == null)
                throw new Exception("Não foi possível encontrar os artigos"); 

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
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<ActionResult> GetById(int id)
    {
        try
        {
            var query = new GetArticleByIdQuery { ID = id };
            var dto = await mediator.Send(query);

            if(dto == null)
                throw new Exception("Artigo não existe!");

            var response = BaseResponseAPI.Create(dto);
            return CustomResponse(response);
        }
        catch(Exception ex)
        {
            return CustomResponseException(ex);
        }
    }
    #endregion

    #region GetByName

    [HttpGet("name/{name}")]
    public async Task<ActionResult> GetByName(string name)
    {
        try
        {
            var query = new GetArticleByNameQuery { Name = name };
            var dto  = await mediator.Send(query);

            if(dto == null)
                throw new Exception("Artigo não existe!");

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
