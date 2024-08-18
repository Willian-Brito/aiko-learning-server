using AikoLearning.Core.Application.Categories.Commands;
using AikoLearning.Presentation.WebAPI.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AikoLearning.Presentation.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : CustomController
{
    #region Properties
    private readonly IMediator mediator;
    #endregion

    #region Constructor
    public CategoryController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    #endregion

    #region Actions

    #region Commands

    #region Create

    // [Authorize(Roles = "Admin" )]
    [HttpPost]
    public async Task<ActionResult> Create(CreateCategoryCommand command)
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

    #region Update
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, UpdateCategoryCommand command)
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
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var command = new DeleteCategoryCommand{ ID = id };
            await mediator.Send(command);

            var response = BaseResponseAPI.Create("Categoria removida com sucesso!");
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

    #region GetAll
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var query = new GetAllCategoriesQuery();
            var dtos  = await mediator.Send(query);

            if (dtos == null)
                throw new Exception("Não foi possível encontrar as categorias"); 

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
            var query = new GetCategoryByIdQuery { ID = id };
            var dto = await mediator.Send(query);

            if(dto == null)
                throw new Exception("Categoria não existe!");

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
            var query = new GetCategoryByNameQuery { Name = name };
            var dto  = await mediator.Send(query);

            if(dto == null)
                throw new Exception("Categoria não existe!");

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