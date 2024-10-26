using apb97.github.io.SimpleResxToJson.Shared.IO;

namespace apb97.github.io.SimpleResxToJson.Shared;

public sealed class MultipleResxConverter(SingleResxConverter singleResxConverter, SearchOption searchOption = SearchOption.TopDirectoryOnly)
{
    public MultipleResxConverter(ResxConverterOptions options)
        : this(new SingleResxConverter(options.Silent ? NullOutput.Instance : ConsoleOutput.Instance),
              options.Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
    { }

    private const string ResxSearchPattern = "*.resx";
    
    public Task ProcessAsync(string inputPath, string? outputPath)
    {
        if (Directory.Exists(inputPath))
        {
            return ProcessMultipleFilesAsync(inputPath, outputPath);
        }
    
        return singleResxConverter.ProcessSingleFileAsync(outputPath, inputPath, Path.GetDirectoryName(inputPath));
    }

    private async Task ProcessMultipleFilesAsync(string inputPath, string? outputPath)
    {
        foreach (var file in Directory.EnumerateFiles(inputPath, ResxSearchPattern, searchOption))
        {
            await singleResxConverter.ProcessSingleFileAsync(outputPath, file, inputPath);
        }
    }
}
