using System.Text;
using Newtonsoft.Json;

namespace AikoLearning.Presentation.WebAPI.Extencions.Converters;

public class ByteArrayJsonConverter : JsonConverter<byte[]>
{
    public override byte[]? ReadJson(
        JsonReader reader, 
        Type objectType, 
        byte[]? existingValue, 
        bool hasExistingValue, 
        JsonSerializer serializer
    )
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        var stringBytes = Encoding.UTF8.GetBytes(reader.Value.ToString());        
        var base64 = Convert.ToBase64String(stringBytes);

        return Convert.FromBase64String(base64);
    }

    public override void WriteJson(JsonWriter writer, byte[] value, JsonSerializer serializer)
    {
        if (value == null)
            writer.WriteNull();        

        var resultString = Encoding.UTF8.GetString(value);

        // var base64 = Convert.ToBase64String(value);
        writer.WriteValue(resultString);
    }
}
