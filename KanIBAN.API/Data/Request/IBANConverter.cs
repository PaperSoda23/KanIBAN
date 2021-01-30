using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KanIBAN.API.Data.Request
{
    public class IBANConverter : JsonConverter<RawIBAN>
    {
        public override RawIBAN Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new RawIBAN { IBAN = reader.GetString() };
        }

        public override void Write(Utf8JsonWriter writer, RawIBAN value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.IBAN);
        }
    }
}