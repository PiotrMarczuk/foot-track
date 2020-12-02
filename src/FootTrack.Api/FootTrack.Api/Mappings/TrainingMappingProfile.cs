using AutoMapper;
using FootTrack.Api.Dtos.Responses;
using FootTrack.BusinessLogic.Models.Training;
using FootTrack.Communication.Dtos;
using TrainingData = FootTrack.BusinessLogic.Models.Training.TrainingData;

namespace FootTrack.Api.Mappings
{
    public class TrainingMappingProfile : Profile
    {
        public TrainingMappingProfile()
        {
            CreateMap<TrainingRecordDto, TrainingRecord>();

            CreateMap<TrainingRecord, TrainingRecordDto>();

            CreateMap<TrainingData, TrainingDataDto>();

            CreateMap<Training, TrainingDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id.Value));
        }
    }
}