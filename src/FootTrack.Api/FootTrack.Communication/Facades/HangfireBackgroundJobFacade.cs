using FootTrack.Communication.JobExecutors;
using FootTrack.Shared;
using Hangfire;

namespace FootTrack.Communication.Facades
{
    public class HangfireBackgroundJobFacade : IHangfireBackgroundJobFacade
    {
        public Result<string> EnqueueJob()
        { 
            return Result.Ok(BackgroundJob.Enqueue<IJobExecutor>(jobExecutor => jobExecutor.Execute()));
        }

        public Result DeleteJob(string jobId)
        {
            return Result.Ok(BackgroundJob.Delete(jobId));
        }
    }
}