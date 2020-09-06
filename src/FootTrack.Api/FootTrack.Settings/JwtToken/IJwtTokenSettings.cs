using System;

namespace FootTrack.Settings.JwtToken
{
    public interface IJwtTokenSettings
    {
        string Secret { get; set; }

        TimeSpan TokenLifetime { get; set; }
    }
}