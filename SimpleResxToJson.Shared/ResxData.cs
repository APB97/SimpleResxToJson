using System.Text.Json.Serialization;

namespace apb97.github.io.SimpleResxToJson.Shared;

public record ResxData
{
    [JsonInclude]
    [JsonPropertyName(nameof(Files))]
    public required Dictionary<string, ResxFileInfo> Files { get; set; }
    [JsonInclude]
    [JsonPropertyName(nameof(Strings))]
    public required Dictionary<string, ResxString> Strings { get; set; }
}
