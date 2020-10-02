using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootTrack.Communication.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.EventHubs;

namespace FootTrack.Communication
{
    public class TrainingJobExecutor : IJobExecutor
    {
        private readonly EventHubClient _eventHubClient;
        private readonly IHubContext<TrainingHub> _hubContext;

        public TrainingJobExecutor(EventHubClient eventHubClient, IHubContext<TrainingHub> hubContext)
        {
            _eventHubClient = eventHubClient;
            _hubContext = hubContext;
        }

        public async Task Execute()
        {
            EventHubRuntimeInformation runtimeInfo = await _eventHubClient.GetRuntimeInformationAsync();
            string[] d2CPartitions = runtimeInfo.PartitionIds;
            Task.WaitAll(d2CPartitions.Select(ReceiveMessagesFromDeviceAsync)
                .ToArray());
        }

        private async Task ReceiveMessagesFromDeviceAsync(string partition)
        {
            PartitionReceiver eventHubReceiver =
                _eventHubClient.CreateReceiver("$Default", partition, EventPosition.FromEnd());

            while (true)
            {
                IEnumerable<EventData> events = await eventHubReceiver.ReceiveAsync(1337);

                if (events == null)
                {
                    continue;
                }

                foreach (EventData eventData in events)
                {
                    string data = Encoding.UTF8.GetString(eventData.Body.Array!);
                    await _hubContext.Clients.All.SendAsync("TrainingMessage", new
                    {
                        data,
                    });
                }
            }
        }
    }
}