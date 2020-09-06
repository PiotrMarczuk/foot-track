﻿using System.Text;
using System.Threading.Tasks;

using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.BusinessLogic.Services;
using FootTrack.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;



namespace FootTrack.Api.Installers
{
    // ReSharper disable once UnusedType.Global
    // ReSharper disable once UnusedMember.Global
    public class JwtInstaller : IInstaller

    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            string secret = configuration.GetSection("JwtTokenSettings")["Secret"];

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
                ValidateLifetime = true,
            };
        }

        private static JwtBearerEvents SetupJwtBearerEvent()
        {
            return new JwtBearerEvents
            {
                OnTokenValidated = async context =>
                await Task.Run(() =>
                {
                    var userService = context.HttpContext.RequestServices
                        .GetRequiredService<IUserService>();
                    Maybe<string> userIdOrNothing = context.Principal.Identity.Name;
                    var result = userIdOrNothing.ToResult(Errors.General.Empty(nameof(Id)))
                        .OnSuccess(async userId => await userService.GetByIdAsync(Id.Create(userId).Value));
                    if (result.IsFailure)
                    {
                        context.Fail(result.Error.Message);
                    } 
                })
            };
        }
    }
}