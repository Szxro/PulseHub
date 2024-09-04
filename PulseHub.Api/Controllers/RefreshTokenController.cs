using MediatR;
using Microsoft.AspNetCore.Mvc;
using PulseHub.Api.Common;
using PulseHub.Api.Extensions;
using PulseHub.Application.Common.DTOs.Requests.RefreshToken;
using PulseHub.Application.Common.DTOs.Responses;
using PulseHub.Application.RefreshTokens.Commands.RegenerateToken;
using PulseHub.SharedKernel;

namespace PulseHub.Api.Controllers
{
    [Route("api/refresh-token")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {
        private readonly ISender _sender;

        public RefreshTokenController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("regenerate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> RegenerateToken(RegenerateTokenRequest tokenResponse)
        {
            RegenerateTokenCommand command = new RegenerateTokenCommand(tokenResponse.token,tokenResponse.refreshToken);

            Result<TokenResponse> result = await _sender.Send(command);

            return result.Match(
                onSuccess: () => CustomResult.Success(result),
                onFailure: CustomResult.Problem);
        }
    }
}
