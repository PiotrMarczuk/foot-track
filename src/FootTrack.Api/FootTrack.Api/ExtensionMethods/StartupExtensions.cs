using FootTrack.Settings;

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
            var urlSettings = new UrlSettings();

            configuration
                .GetSection(nameof(UrlSettings))
                .Bind(urlSettings);

            services.AddCors(options =>
            {
                options.AddPolicy(
                    policyName, 
                    builder=>
                    {
                        builder
                            .WithOrigins(urlSettings.ClientUrl)
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
        }
    }
}
