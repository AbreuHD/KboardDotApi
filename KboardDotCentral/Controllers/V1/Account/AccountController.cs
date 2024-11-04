using Core.Application.Features.Identity.AuthenticateEmail.Command.AuthEmail;
using Core.Application.Features.Identity.Login.Queries.AuthLogin;
using Core.Application.Features.Identity.Register.Commands.CreateAccount;
using Core.Application.Features.Identity.Register.Commands.SendValidationEmailAgain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KboardDotCentral.Controllers.V1.Account
{
    public class AccountController(IMediator mediator, ILogger<AccountController> logger) : BaseController
    {
        public new IMediator Mediator { get; } = mediator;
        private readonly ILogger<AccountController> _logger = logger;

        [HttpPost("Login")]
        public async Task<IActionResult> AuthLogin([FromBody] AuthLoginQuery request)
        {
            var data = await Mediator.Send(request);
            return Ok(data);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateAccountCommand request)
        {
            var data = await Mediator.Send(request);
            return Ok(data);
        }

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] AuthEmailCommand request)
        {
            var data = await Mediator.Send(request);
            return Ok(data);
        }

        [HttpPost("ResentConfirmation")]
        public async Task<IActionResult> ResentConfirmation([FromBody] SendValidationEmailAgainCommand request)
        {
            var data = await Mediator.Send(request);
            return Ok(data);
        }
    }
}
