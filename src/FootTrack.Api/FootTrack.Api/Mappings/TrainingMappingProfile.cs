using AutoMapper;
using FootTrack.BusinessLogic.Models.Training;
using FootTrack.Communication.Dtos;

namespace FootTrack.Api.Mappings
{
    public class TrainingMappingProfile : Profile
    {
        public TrainingMappingProfile()
        {
            CreateMap<TrainingRecordDto, TrainingRecord>();

            CreateMap<TrainingRecord, FootTrack.Database.Models.TrainingData>();
        }
    }
}