using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.Communication.Facades
{
    public interface IHangfireBackgroundJobFacade
    {
        Result<Id> EnqueueJob();

        Result DeleteJob(Id jobId);
    }
}