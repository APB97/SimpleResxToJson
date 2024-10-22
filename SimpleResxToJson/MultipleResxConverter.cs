namespace apb97.github.io.SimpleResxToJson.Shared;

public class MultipleResxConverter(SingleResxConverter singleResxConverter)
{
    private const string ResxSearchPattern = "*.resx";

    public SearchOption DirectorySearchOption { get; set; } = SearchOption.AllDirectories;

    public async Task ProcessMultipleFiles(string? outputPath, string inputPath)
    {
        foreach (var file in Directory.EnumerateFiles(inputPath, ResxSearchPattern, DirectorySearchOption))
        {
            await singleResxConverter.ProcessSingleFile(outputPath, file, inputPath);
        }
    }
}
