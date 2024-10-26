using apb97.github.io.SimpleResxToJson.Shared.IO;

namespace apb97.github.io.SimpleResxToJson.Shared;

public sealed class MultipleResxConverter(ResxConverterOptions options)
{
    private const string ResxSearchPattern = "*.resx";

    private readonly IOutput output = options.Silent ? NullOutput.Instance : ConsoleOutput.Instance;
    private readonly SingleResxConverter singleResxConverter = new(options);
    private readonly SearchOption searchOption = options.Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

    public Task ProcessAsync(string inputPath, string? outputPath)
    {
        if (Directory.Exists(inputPath))
        {
            return ProcessMultipleFilesAsync(inputPath, outputPath);
        }
        else if (File.Exists(inputPath))
        {
            return singleResxConverter.ProcessSingleFileAsync(outputPath, inputPath, Path.GetDirectoryName(inputPath));
        }

        output.PrintMessage("Neither a file nor a directory exist at: {0}", inputPath);
        return Task.CompletedTask;
    }

    private async Task ProcessMultipleFilesAsync(string inputPath, string? outputPath)
    {
        foreach (var file in Directory.EnumerateFiles(inputPath, ResxSearchPattern, searchOption))
        {
            await singleResxConverter.ProcessSingleFileAsync(outputPath, file, inputPath);
        }
    }
}
