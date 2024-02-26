using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WY.Common.Token
{
    public class JwtTokenOptions
    {
        [JsonPropertyName("secret")]
        public string Secret { get; set; }

        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        [JsonPropertyName("audience")]
        public string Audience { get; set; }

        [JsonPropertyName("accessExpiration")]
        public int AccessExpiration { get; set; }

        [JsonPropertyName("refreshExpiration")]
        public int RefreshExpiration { get; set; }
    }
}
