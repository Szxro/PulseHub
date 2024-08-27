﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using PulseHub.Application.Users.Commands.CreateUser;
using PulseHub.SharedKernel;

namespace PulseHub.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Result>> CreateUser(CreateUserCommand userCommand)
        {
            Result result = await _mediator.Send(userCommand);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
