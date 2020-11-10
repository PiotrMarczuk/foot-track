using FootTrack.BusinessLogic.Models.ValueObjects;

namespace FootTrack.BusinessLogic.Models.Training
{
    public class GetTrainingsForUserParameters
    {
        public Id UserId { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetTrainingsForUserParameters(Id userId, int pageNumber, int pageSize)
        {
            UserId = userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}