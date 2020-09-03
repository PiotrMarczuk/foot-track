using System;

namespace FootTrack.Settings.JwtToken
{
    public class JwtTokenSettings : IJwtTokenSettings
    {
        public string Secret { get; set; }

        public TimeSpan TokenLifetime { get; set; }
    }
}
