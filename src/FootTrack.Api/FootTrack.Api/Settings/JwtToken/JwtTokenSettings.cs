using System;

namespace FootTrack.Api.Settings.JwtToken
{
    public class JwtTokenSettings : IJwtTokenSettings
    {
        public string Secret { get; set; }

        public TimeSpan TokenLifetime { get; set; }
    }
}
