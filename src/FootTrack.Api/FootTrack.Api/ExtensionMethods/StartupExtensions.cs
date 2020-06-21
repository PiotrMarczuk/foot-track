using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using FootTrack.Api.Attributes;
using FootTrack.Api.Exceptions;
using FootTrack.Api.Mappings;
using FootTrack.Api.Repositories.Implementations;
using FootTrack.Api.Repositories.Interfaces;
using FootTrack.Api.Services.Implementations;
using FootTrack.Api.Services.Interfaces;
using FootTrack.Api.Settings.JwtToken;
using FootTrack.Api.Settings.MongoDb;

namespace FootTrack.Api.ExtensionMethods
{
    public static class StartupExtensions
    {

        public static void LoadConfigs(this IServiceCollection services)
        {
            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddSingleton<IJwtTokenSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<JwtTokenSettings>>().Value);
        }

        public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.Configure<JwtTokenSettings>(configuration.GetSection("JwtTokenSettings"));
        }

        public static void ServicesConfiguration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddTransient(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddTransient<ModelValidationFilterAttribute>();
        }

        public static void ConfigureMapper(this IServiceCollection services)
        {
            var mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            }).CreateMapper();

            services.AddSingleton(mapper);
        }

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration.GetSection("JwtTokenSettings")["Secret"];

            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                            var userId = context.Principal.Identity.Name;
                            try
                            {
                                await userService.GetByIdAsync(userId);
                            }
                            catch (NotFoundException)
                            {
                                context.Fail("Unauthorized");
                            }
                        }
                    };
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}
