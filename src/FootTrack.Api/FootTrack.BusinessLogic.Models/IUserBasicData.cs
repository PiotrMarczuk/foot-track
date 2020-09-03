using FootTrack.BusinessLogic.Models.ValueObjects;

namespace FootTrack.BusinessLogic.Models
{
    public interface IUserBasicData
    {
        public Id Id { get; }
        
        public Email Email { get; }
    }
}