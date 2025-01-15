namespace apb97.github.io.SimpleResxToJson.Shared;

public readonly record struct ResxConverterOptions
{
    public readonly bool Recursive { get; init; }
    public readonly bool Silent { get; init; }
    public readonly bool SkipNonStrings { get; init; }
    public readonly bool StringDataOnly { get; init; }
}
