using System;
using FootTrack.Api.Utils;

namespace FootTrack.Api.IntegrationTests.Utils
{
    internal class ApiResponse<T>
    {
        public T Result { get; set; }

        public ErrorDetails Errors { get; set; }

        public DateTime TimeGenerated { get; set; }
    }
}
