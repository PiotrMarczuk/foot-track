using System;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using FootTrack.Api.Exceptions;

namespace FootTrack.Api.Installers
{
    public class ProblemDetailsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddProblemDetails(x =>
            {
                x.Map<NotFoundException>(ex => new StatusCodeProblemDetails(StatusCodes.Status404NotFound));
                x.Map<ArgumentException>(ex => new StatusCodeProblemDetails(StatusCodes.Status400BadRequest));
                x.Map<WrongCredentialsException>(ex => new StatusCodeProblemDetails(StatusCodes.Status401Unauthorized));
                x.Map<AlreadyExistsException>(ex => new StatusCodeProblemDetails(StatusCodes.Status409Conflict));
            });
        }
    }
}
