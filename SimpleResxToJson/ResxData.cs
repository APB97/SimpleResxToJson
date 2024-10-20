using System.Text.Json.Serialization;

namespace apb97.github.io.SimpleResxToJson.Shared;

public record ResxData
{
    [JsonInclude]
    [JsonPropertyName(nameof(Strings))]
    public required Dictionary<string, string> Strings { get; set; }
}

[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(Dictionary<string, string>))]
[JsonSerializable(typeof(ResxData))]
public partial class ResxDataContext : JsonSerializerContext { }