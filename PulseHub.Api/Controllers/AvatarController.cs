using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseHub.Api.Common;
using PulseHub.Api.Extensions;
using PulseHub.Application.Avatars.Commands.CreateAvatar;
using PulseHub.SharedKernel;

namespace PulseHub.Api.Controllers;

[Route("api/avatar")]
[ApiController]
public class AvatarController : ControllerBase
{
    private readonly ISender _sender;

    public AvatarController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("upload")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> UploadAvatar(IFormFile formFile)
    {
        Result result = await _sender.Send(new CreateAvatarCommand(formFile));

        return result.Match(
            onSuccess: () => Results.Created(),
            onFailure: CustomResult.Problem);
    }
}
