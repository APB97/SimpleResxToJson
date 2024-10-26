namespace apb97.github.io.SimpleResxToJson.Shared
{
    public record ResxFileInfo
    {
        public required string Value { get; set; }
        public required string Type { get; set; }
        public string? Encoding { get; set; }
        public string? Comment { get; set; }
    }
}