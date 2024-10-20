using System.Text.Json;
using System.Xml;

namespace apb97.github.io.SimpleResxToJson.Shared;

public static class ResxConverter
{
    public static Task WriteAsJsonToStreamAsync(Stream resxInputStream, Stream jsonOutput)
    {
        var keyValueDictionary = LoadStrings(resxInputStream);
        return JsonSerializer.SerializeAsync(jsonOutput, new ResxData { Strings = keyValueDictionary }, typeof(ResxData), ResxDataContext.Default);
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
