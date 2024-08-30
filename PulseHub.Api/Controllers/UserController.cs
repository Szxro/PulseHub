using MediatR;
using Microsoft.AspNetCore.Mvc;
using PulseHub.Api.Extensions;
using PulseHub.Api.Common;
using PulseHub.Application.Common.DTOs.Requests.Users;
using PulseHub.Application.Users.Commands.CreateUser;
using PulseHub.SharedKernel;

namespace PulseHub.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("create-user")]
        public async Task<IResult> CreateUser(CreateUserRequest request)
        {
            CreateUserCommand command = new CreateUserCommand(
                request.firstName,
                request.lastName,
                request.username,
                request.email,
                request.password);

            Result result = await _sender.Send(command);

            return result.Match(
                onSuccess: () => CustomResult.Success(result),
                onFailure: CustomResult.Problem
            );
        }
    }
}
