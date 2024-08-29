using MediatR;
using Microsoft.AspNetCore.Mvc;
using PulseHub.Application.EmailCodes.Commands.ResendEmailCode;
using PulseHub.Application.EmailCodes.Commands.VerifyEmailCode;
using PulseHub.SharedKernel;

namespace PulseHub.Api.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ISender _sender;

        public EmailController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("verify")]
        public async Task<ActionResult<Result>> VerifyEmailCode([FromQuery] string code)
        {
            Result response = await _sender.Send(new VerifyEmailCodeCommand(code));

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("resend")]
        public async Task<ActionResult<Result>> ResendEmailCode(ResendEmailCodeCommand resendEmail)
        {
            Result response = await _sender.Send(resendEmail);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
