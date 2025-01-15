using System.Text.Json.Serialization;

namespace apb97.github.io.SimpleResxToJson.Shared;

[JsonSerializable(typeof(ResxStringsData))]
[JsonSerializable(typeof(ResxData))]
public partial class ResxDataContext : JsonSerializerContext { }