using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Xml;

namespace apb97.github.io.SimpleResxToJson.Shared;

public static class ResxConverter
{
    private static JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerOptions.Default);
    
    static ResxConverter()
    {
        options.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
    }

    public static Task WriteAsJsonToStreamAsync(Stream resxInputStream, Stream jsonOutput)
    {
        var keyValueDictionary = LoadStrings(resxInputStream);
        var data = new ResxData { Strings = keyValueDictionary };

        return JsonSerializer.SerializeAsync(jsonOutput, data, options);
    }

    public static Dictionary<string, string> LoadStrings(Stream stream)
    {
        using var xml = XmlReader.Create(stream);
        var results = new Dictionary<string, string>();
        while (xml.ReadToFollowing("data"))
        {
            var key = xml.GetAttribute("name");
            xml.ReadToDescendant("value");
            var value = xml.ReadElementContentAsString();
            if (key == null || value == null) continue;
            results[key] = value;
        }
        return results;
    }
}
