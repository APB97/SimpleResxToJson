using System.Text.Json;
using System.Xml;

namespace apb97.github.io.SimpleResxToJson.Shared;

public static class ResxConverter
{
    public static Task WriteAsJsonToStreamAsync(Stream resxInputStream, Stream jsonOutput)
    {
        var keyValueDictionary = LoadStrings(resxInputStream);
        return JsonSerializer.SerializeAsync(jsonOutput, new ResxStringsData { Strings = keyValueDictionary }, typeof(ResxStringsData), ResxDataContext.Default);
    }

    public static Dictionary<string, string> LoadStrings(Stream stream)
    {
        using var xml = XmlReader.Create(stream);
        var results = new Dictionary<string, string>();
        while (xml.ReadToFollowing("data"))
        {
            var key = xml.GetAttribute("name");
            var type = xml.GetAttribute("type");
            if (type != null) continue;
            xml.ReadToDescendant("value");
            var value = xml.ReadElementContentAsString();
            if (key == null || value == null) continue;
            results[key] = value;
        }
        return results;
    }

    public static Task WriteAllAsJsonToStreamAsync(Stream resxInputStream, Stream jsonOutput)
    {
        var allData = LoadAll(resxInputStream);
        return JsonSerializer.SerializeAsync(jsonOutput, allData, typeof(ResxData), ResxDataContext.Default);
    }
    
    public static ResxData LoadAll(Stream stream)
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
            }
        }
        return result;
    }

    private static ResxFileInfo? GetFileInfo(string value, string? comment)
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
