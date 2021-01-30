using System.Text.Json.Serialization;

namespace KanIBAN.API.Data.Request
{
    public class RawIBAN
    {
        [JsonPropertyName("iban")]
        public string IBAN { get; set; }

        public override string ToString() => IBAN;
    }
}