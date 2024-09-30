using Microsoft.AspNetCore.Mvc;
using MediatR;
using PulseHub.SharedKernel;
using PulseHub.Application.Applications.Commands.CreateApplication;
using PulseHub.Application.Common.DTOs.Requests.Application;
using PulseHub.Application.Common.DTOs.Responses;
using PulseHub.Api.Extensions;
using PulseHub.Api.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PulseHub.Api.Controllers
{
    [Route("api/application")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ISender _sender;

        public ApplicationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IResult> CreateApplication(CreateApplicationRequest request)
        {
            Result<ApplicationCreatedResponse> result = await _sender.Send(new CreateApplicationCommand(request.applicationName,
                                                                                                        request.applicationDescription,
                                                                                                        request.providerName,
                                                                                                        request.providerApplicationId));
            return result.Match(
                onSuccess: () => CustomResult.Success(result),
                onFailure: CustomResult.Problem);
        }
    }
}
