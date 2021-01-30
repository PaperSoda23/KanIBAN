using System;
using System.Text.Json.Serialization;

namespace KanIBAN.API.Data.Request
{
    public class ValidateIBANRequest
    {
        [JsonPropertyName("iban")]
        [JsonConverter(typeof(IBANConverter))]
        public RawIBAN RawIBAN { get; set; }
    }
}