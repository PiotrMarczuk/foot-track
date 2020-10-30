using System.Threading.Tasks;

namespace FootTrack.Communication.JobExecutors
{
    public interface IJobExecutor
    {
        Task Execute();
    }
}