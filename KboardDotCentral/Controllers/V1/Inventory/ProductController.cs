using Core.Application.Features.Inventory.Product.Command;
using Core.Application.Features.Inventory.Product.Queries;
using KboardDotCentral.Controllers.V1.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KboardDotCentral.Controllers.V1.Inventory
{
    public class ProductController(IMediator mediator, ILogger<ProductController> logger) : BaseController
    {
        public new IMediator Mediator { get; } = mediator;
        private readonly ILogger<ProductController> _logger = logger;


        [HttpPost()]
        public async Task<IActionResult> Index([FromBody] AddProductCommand command)
        {
            var response = await Mediator.Send(command);
            return StatusCode((response.Statuscode), response);
        }


        [HttpGet("GetDetails")]
        public async Task<IActionResult> Index([FromQuery] GetProductDetailsByIdQuery query)
        {
            var response = await Mediator.Send(query);
            return StatusCode((response.Statuscode), response);
        }

        [HttpGet()]
        public async Task<IActionResult> Index([FromQuery] GetAllProductsQuery query)
        {
            var response = await Mediator.Send(query);
            return StatusCode((response.Statuscode), response);
        }        

        [HttpDelete]
        public async Task<IActionResult> Index([FromBody] DeleteProductCommand command)
        {
            var response = await Mediator.Send(command);
            return StatusCode((response.Statuscode), response);
        }
    }
}
