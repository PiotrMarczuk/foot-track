using System;

namespace FootTrack.Api.Settings.JwtToken
{
    public interface IJwtTokenSettings
    {
        string Secret { get; set; }

        TimeSpan TokenLifetime { get; set; }
    }
}