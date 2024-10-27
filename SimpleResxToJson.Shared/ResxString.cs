namespace apb97.github.io.SimpleResxToJson.Shared
{
    public record ResxString
    {
        public required string Value { get; set; }
        public string? Comment { get; set; }
    }
}