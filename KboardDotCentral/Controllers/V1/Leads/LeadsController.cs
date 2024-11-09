using Core.Application.Features.Identity.AuthenticateEmail.Command.AuthEmail;
using Core.Application.Features.Identity.Login.Queries.AuthLogin;
using Core.Application.Features.Identity.Register.Commands.CreateAccount;
using Core.Application.Features.Identity.Register.Commands.SendValidationEmailAgain;
using Core.Application.Features.Inventory.Product.Command;
using Core.Application.Features.Inventory.Product.Queries;
using Core.Application.Features.Leads.Lead.Command;
using Core.Application.Features.Leads.Lead.Queries;
using KboardDotCentral.Controllers.V1.Inventory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KboardDotCentral.Controllers.V1.Leads
{
    
        public class LeadsController(IMediator mediator, ILogger<LeadsController> logger) : BaseController
        {
        public new IMediator Mediator { get; } = mediator;
        private readonly ILogger<LeadsController> _logger = logger;

        [HttpPost()]
        public async Task<IActionResult> Index([FromBody] AddLeadCommand command)
        {
            var response = await Mediator.Send(command);
            return StatusCode((response.Statuscode), response);
        }

        [HttpGet("GetDetails")]
        public async Task<IActionResult> Index([FromQuery] GetLeadDetailsByIdQuery query)
        {
            var response = await Mediator.Send(query);
            return StatusCode((response.Statuscode), response);
        }

        [HttpGet()]
        public async Task<IActionResult> Index([FromQuery] GetAllLeadsQuery query)
        {
            var response = await Mediator.Send(query);
            return StatusCode((response.Statuscode), response);
        }

        [HttpDelete]
        public async Task<IActionResult> Index([FromBody] DeleteLeadCommand command)
        {
            var response = await Mediator.Send(command);
            return StatusCode((response.Statuscode), response);
        }



    }
}
