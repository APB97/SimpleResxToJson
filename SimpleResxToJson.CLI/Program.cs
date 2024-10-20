using apb97.github.io.SimpleResxToJson.Shared;
using System.Collections.Immutable;

var inputs = args.Where(arg => arg.StartsWith("--input=")).ToImmutableArray();
var output = args.Where(arg => arg.StartsWith("--output=")).ToImmutableArray();
if (inputs.Length > 0)
{
    string? outputPath = null;
    if (output.Length == 1)
    {
        outputPath = output[0].Replace("--output=", string.Empty);
    }

    await ProcessInputs(inputs, outputPath);
}

static async Task ProcessInputs(ImmutableArray<string> inputs, string? outputPath)
{
    foreach (var inputPath in from input in inputs
                              let inputPath = input.Replace("--input=", string.Empty)
                              select inputPath)
    {
        if (Directory.Exists(inputPath))
        {
            await ProcessMultipleFiles(outputPath, inputPath);
        }
        else
        {
            await ProcessSingleFile(outputPath, inputPath, Path.GetDirectoryName(inputPath));
        }
    }
}

static async Task ProcessMultipleFiles(string? outputPath, string inputPath)
{
    foreach (var file in Directory.EnumerateFiles(inputPath, "*.resx", SearchOption.AllDirectories))
    {
        await ProcessSingleFile(outputPath, file, inputPath);
    }
}

static async Task ProcessSingleFile(string? outputPath, string inputPath, string? inputDirectory)
{
    var inputPathParentDirectory = Path.GetDirectoryName(inputPath);
    outputPath ??= Path.Combine(
        inputDirectory == null || inputPathParentDirectory == null
            ? Directory.GetCurrentDirectory()
            : Path.Combine(Directory.GetCurrentDirectory(), Path.GetRelativePath(inputDirectory, inputPathParentDirectory)),
        $"{Path.GetFileNameWithoutExtension(inputPath)}.json");

    Console.WriteLine("Converting {0} into {1} ...", inputPath, outputPath);
    await SingleResxConverter.ConvertFileAsync(inputPath, outputPath);
    Console.WriteLine("Converted {0} into {1}", inputPath, outputPath);
}