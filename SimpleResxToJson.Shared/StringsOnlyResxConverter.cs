using System.Text.Json;
using System.Xml;

namespace apb97.github.io.SimpleResxToJson.Shared;

public class StringsOnlyResxConverter(Func<string?, bool> excludeTypeFunction) : IResxConverter
{
    public StringsOnlyResxConverter(ResxConverterOptions options)
        : this(options.SkipNonStrings ? t => t != null : _ => false) { }

    public Task WriteAsJsonToStreamAsync(Stream resxInputStream, Stream jsonOutput)
    {
        ResxStringsData data = new() { Strings = LoadStrings(resxInputStream) };
        return JsonSerializer.SerializeAsync(jsonOutput, data, typeof(ResxStringsData), ResxDataContext.Default);
    }

    public Dictionary<string, string> LoadStrings(Stream stream)
    {
        using var xml = XmlReader.Create(stream);
        var results = new Dictionary<string, string>();
        while (xml.ReadToFollowing("data"))
        {
            var key = xml.GetAttribute("name");
            var type = xml.GetAttribute("type");
            if (excludeTypeFunction(type)) continue;
            xml.ReadToDescendant("value");
            var value = xml.ReadElementContentAsString();
            if (key == null || value == null) continue;
            results[key] = value;
        }
        return results;
    }
}