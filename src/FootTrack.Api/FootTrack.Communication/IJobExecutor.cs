using System.Threading.Tasks;

namespace FootTrack.Communication
{
    public interface IJobExecutor
    {
        Task Execute();
    }
}