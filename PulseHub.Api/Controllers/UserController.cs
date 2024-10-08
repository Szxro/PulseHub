﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using PulseHub.Api.Extensions;
using PulseHub.Api.Common;
using PulseHub.Application.Common.DTOs.Requests.Users;
using PulseHub.Application.Users.Commands.CreateUser;
using PulseHub.SharedKernel;
using PulseHub.Application.Users.Commands.LoginUser;
using PulseHub.Application.Common.DTOs.Responses;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                onSuccess: Results.Created,
                onFailure: CustomResult.Problem
            );
        }

        [HttpPost("login-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> LoginUser(LoginUserRequest loginRequest)
        {
            LoginUserCommand command = new LoginUserCommand(loginRequest.username,loginRequest.password);

            Result<TokenResponse> result = await _sender.Send(command);

            return result.Match(
                onSuccess: () => CustomResult.Success(result),
                onFailure: CustomResult.Problem);
        }
    }
}
