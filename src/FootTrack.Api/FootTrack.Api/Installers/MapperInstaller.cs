using AutoMapper;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using FootTrack.Api.Mappings;

namespace FootTrack.Api.Installers
{
    public class MapperInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            IMapper mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserMappingProfile());
            }).CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
