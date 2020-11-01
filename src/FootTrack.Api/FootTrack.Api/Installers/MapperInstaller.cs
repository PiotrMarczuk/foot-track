using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FootTrack.Api.Mappings;

namespace FootTrack.Api.Installers
{
    // ReSharper disable once UnusedType.Global
    // ReSharper disable once UnusedMember.Global
    public class MapperInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            IMapper mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfiles(
                    new List<Profile>
                    {
                        new UserMappingProfile(),
                        new TrainingMappingProfile(),
                    });
            }).CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}