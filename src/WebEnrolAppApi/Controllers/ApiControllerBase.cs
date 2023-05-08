using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebEnrolAppApi.Controllers;


[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[Authorize]
public abstract class ApiControllerBase : ControllerBase
{
    private IMediator _mediator = null!;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}
