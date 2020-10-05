using FootTrack.Shared;

namespace FootTrack.Communication.Facades
{
    public interface IHangfireBackgroundJobFacade
    {
        Result<string> EnqueueJob();

        Result DeleteJob(string jobId);
    }
}