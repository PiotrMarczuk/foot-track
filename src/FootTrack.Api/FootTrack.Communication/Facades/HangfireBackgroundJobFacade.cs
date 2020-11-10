using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Communication.JobExecutors;
using FootTrack.Shared;
using Hangfire;

namespace FootTrack.Communication.Facades
{
    public class HangfireBackgroundJobFacade : IHangfireBackgroundJobFacade
    {
        public Result<Id> EnqueueJob()
        {
            string literalJobId = BackgroundJob.Enqueue<IJobExecutor>(jobExecutor => jobExecutor.Execute());
            Id jobId = Id.Create(literalJobId).Value;
            return Result.Ok(jobId);
        }

        public Result DeleteJob(Id jobId)
        {
            return Result.Ok(BackgroundJob.Delete(jobId));
        }
    }
}