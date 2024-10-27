using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KboardDotCentral.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/es/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
