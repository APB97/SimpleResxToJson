using System.Text.Json.Serialization;

namespace apb97.github.io.SimpleResxToJson.Shared;

public record ResxStringsData
{
    [JsonInclude]
    [JsonPropertyName(nameof(Strings))]
    public required Dictionary<string, string> Strings { get; set; }
}
