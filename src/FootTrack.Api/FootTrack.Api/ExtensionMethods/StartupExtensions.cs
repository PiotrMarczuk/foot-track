using FootTrack.Api.Settings;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootTrack.Api.ExtensionMethods
{
    public static class StartupExtensions
    {
        public static void ConfigureCors(
            this IServiceCollection services,
            IConfiguration configuration,
            string policyName)
        {
            var urlSettings = configuration.GetSection("UrlSettings");
            services.Configure<UrlSettings>(urlSettings);
            var clientUrl = urlSettings.Get<UrlSettings>().ClientUrl;

            services.AddCors(options =>
            {
                options.AddPolicy(
                    policyName, 
                    builder=>
                    {
                        builder
                            .WithOrigins(clientUrl)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
        }
    }
}
