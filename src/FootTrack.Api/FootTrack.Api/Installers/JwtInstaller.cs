using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using FootTrack.Api.Exceptions;
using FootTrack.Api.Services.Interfaces;

namespace FootTrack.Api.Installers
{
    public class JwtInstaller : IInstaller

    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration.GetSection("JwtTokenSettings")["Secret"];

            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Events = SetupJwtBearerEvent();
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = SetupTokenValidationParameters(key);
                });
        }

        private static TokenValidationParameters SetupTokenValidationParameters(byte[] key)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
        }

        private static JwtBearerEvents SetupJwtBearerEvent()
        {
            return new JwtBearerEvents
            {
                OnTokenValidated = async context =>
                {
                    var userService = context.HttpContext.RequestServices
                        .GetRequiredService<IUserService>();
                    var userId = context.Principal.Identity.Name;
                    try
                    {
                        await userService
                            .GetByIdAsync(userId);
                    }
                    catch (NotFoundException)
                    {
                        context.Fail("Unauthorized");
                    }
                }
            };
        }
    }
}

