using System.Text.Json.Serialization;

namespace apb97.github.io.SimpleResxToJson.Shared;

[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(Dictionary<string, string>))]
[JsonSerializable(typeof(ResxStringsData))]
[JsonSerializable(typeof(ResxData))]
[JsonSerializable(typeof(ResxFileInfo))]
[JsonSerializable(typeof(ResxString))]
public partial class ResxDataContext : JsonSerializerContext { }