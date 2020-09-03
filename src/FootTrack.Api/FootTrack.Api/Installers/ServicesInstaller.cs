using FootTrack.BusinessLogic.Services.Implementations;
using FootTrack.BusinessLogic.Services.Interfaces;
using FootTrack.Repository;

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
                .AddScoped<IUserRepository, UserRepository>();
            services
                .AddTransient(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            services
                .AddTransient<IUserService, UserService>();
            services
                .AddTransient<IJwtTokenService, JwtTokenService>();
        }
    }
}
