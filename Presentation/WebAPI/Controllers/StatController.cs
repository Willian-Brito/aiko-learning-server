using AikoLearning.Core.Application.Stats;
using AikoLearning.Presentation.WebAPI.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AikoLearning.Presentation.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StatController : CustomController
{
    #region Properties
    private readonly IMediator mediator;
    #endregion

    #region Constructors
    public StatController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    #endregion

    #region Actions

    [HttpGet]
    public async Task<ActionResult> GetLast()
    {
        var query = new GetLastStatQuery();
        var dtos  = await mediator.Send(query);
        var response = BaseResponseAPI.Create(dtos);
        
        return CustomResponse(response);
    }
    #endregion
}
