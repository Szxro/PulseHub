using MediatR;
using Microsoft.AspNetCore.Mvc;
using PulseHub.Api.Extensions;
using PulseHub.Api.Common;
using PulseHub.Application.Common.DTOs.Requests.EmailCodes;
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

        // If you are writing APIs without MVC use IResult
        [HttpPost("verify")]
        public async Task<IResult> VerifyEmailCode(VerifyEmailCodeRequest request)
        {
            VerifyEmailCodeCommand command = new VerifyEmailCodeCommand(request.code);

            Result result = await _sender.Send(command);

            return result.Match(
                onSuccess: () => Results.Ok(),
                onFailure: CustomResult.Problem
             );
        }

        [HttpPost("resend")]
        public async Task<IResult> ResendEmailCode(ResendEmailRequest request)
        {
            ResendEmailCodeCommand command = new ResendEmailCodeCommand(request.username, request.email);

            Result result = await _sender.Send(command);

            return result.Match(
                onSuccess: () => Results.Ok(),
                onFailure: CustomResult.Problem
            );
        }
    }
}
