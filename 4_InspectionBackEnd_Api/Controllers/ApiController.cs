using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _4_InspectionBackEnd_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator? _mediator;
        /// <summary>
        /// init mediator
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
    }
}