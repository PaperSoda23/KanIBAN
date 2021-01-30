using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KanIBAN.API.Data.Request
{
    public class ValidateIBANListRequest
    {
        [JsonPropertyName("ibans")]
        public IList<string>  RawIBANs { get; set; }
    }
}