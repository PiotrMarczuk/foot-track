using FootTrack.Api.Attributes;
using FootTrack.Api.Repositories.Implementations;
using FootTrack.Api.Repositories.Interfaces;
using FootTrack.Api.Services.Implementations;
using FootTrack.Api.Services.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootTrack.Api.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services
                .AddTransient(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            services
                .AddTransient<IUserService, UserService>();
            services
                .AddTransient<IJwtTokenService, JwtTokenService>();
            services
                .AddTransient<ModelValidationFilterAttribute>();
        }
    }
}
