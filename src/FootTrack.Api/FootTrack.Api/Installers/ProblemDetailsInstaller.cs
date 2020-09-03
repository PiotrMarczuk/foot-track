using System;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootTrack.Api.Installers
{
    public class ProblemDetailsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddProblemDetails(x =>
            {
                x.Map<ArgumentException>(ex =>
                    new StatusCodeProblemDetails(StatusCodes.Status500InternalServerError));
            });
        }
    }
}