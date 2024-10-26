using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace apb97.github.io.SimpleResxToJson.Shared;

public class AllDataResxConverter : IResxConverter
{
    private readonly ResxDataContext context;
    private readonly ResxConverterOptions options;

    public AllDataResxConverter(ResxConverterOptions options)
    {
        context = new ResxDataContext(new JsonSerializerOptions(JsonSerializerOptions.Default) { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        this.options = options;
    }

    public Task WriteAsJsonToStreamAsync(Stream resxInputStream, Stream jsonOutput)
    {
        var allData = LoadAll(resxInputStream);
        return JsonSerializer.SerializeAsync(jsonOutput, allData, typeof(ResxData), context);
    }
    
    public ResxData LoadAll(Stream stream)
    {
        using var xml = XmlReader.Create(stream);
        var result = new ResxData { Strings = [], Files = [] };
        while (xml.ReadToFollowing("data"))
        {
            var key = xml.GetAttribute("name");
            var type = xml.GetAttribute("type");
            xml.ReadToDescendant("value");
            var value = xml.ReadElementContentAsString();
            var comment = xml.ReadToNextSibling("comment") ? xml.ReadElementContentAsString() : null;
            if (key == null || value == null) continue;
            switch (type)
            {
                case "System.Resources.ResXFileRef, System.Windows.Forms":
                    ResxFileInfo? resxFileInfo = GetFileInfo(value, comment);
                    if (resxFileInfo is not null)
                        result.Files[key] = resxFileInfo;
                    continue;
                case null:
                    result.Strings[key] = new ResxString { Value = value, Comment = comment};
                    break;
                default:
                    if (options.SkipNonStrings) continue;
                    result.Strings[key] = new ResxString { Value = value, Comment = comment };
                    break;
            }

        }
        return result;
    }

    public static ResxFileInfo? GetFileInfo(string value, string? comment)
    {
        string[] splitParts = value.Split(';');
        if (splitParts.Length < 2 || splitParts.Length > 3) return null;
        if (splitParts.Length == 2)
        {
            return new ResxFileInfo
            {
                Value = splitParts[0],
                Type = splitParts[1],
                Comment = comment
            };
        }
        return new ResxFileInfo
        {
            Value = splitParts[0],
            Type = splitParts[1],
            Encoding = splitParts[2],
            Comment = comment
        };
    }
}
