using Core.Application.Features.Inventory.Miscellaneous.Command;
using Core.Application.Features.Inventory.Miscellaneous.Queries;
using Core.Application.Features.Inventory.Product.Command;
using KboardDotCentral.Controllers.V1.Inventory;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KboardDotCentral.Controllers.V1.Miscellaneous
{
    public class MiscellaneousController(IMediator mediator, ILogger<MiscellaneousController> logger) : BaseController
    {
        public new IMediator Mediator { get; } = mediator;
        private readonly ILogger<MiscellaneousController> _logger = logger;

        [HttpPost("AddTax")]
        public async Task<IActionResult> AddTax([FromBody] CreateTaxCommand command)
        {
            var response = await Mediator.Send(command);
            return StatusCode((response.Statuscode), response);
        }

        [HttpPost("AddTrackingType")]
        public async Task<IActionResult> AddTrackingType([FromBody] TrackingTypeCommand command)
        {
            var response = await Mediator.Send(command);
            return StatusCode((response.Statuscode), response);
        }

        [HttpGet("GetAllTax")]
        public async Task<IActionResult> GetAllTax()
        {
            var response = await Mediator.Send(new GetAllTaxesQuery { });
            return StatusCode((response.Statuscode), response);
        }

        [HttpGet("GetAllTrackingType")]
        public async Task<IActionResult> GetAllTrackingType()
        {
            var response = await Mediator.Send(new GetAllTrackingTypeQuery { });
            return StatusCode((response.Statuscode), response);
        }
    }
}
