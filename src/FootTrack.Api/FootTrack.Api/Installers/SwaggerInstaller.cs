using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FootTrack.Api.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "FootTrack API",
                        Version = "v1"
                    });
            });
        }
    }
}
