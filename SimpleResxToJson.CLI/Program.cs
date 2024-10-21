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
    outputPath ??= Directory.GetCurrentDirectory();
    string destinationPath;
    if (inputDirectory != null && inputPathParentDirectory != null)
        destinationPath = Path.Combine(outputPath, Path.GetRelativePath(inputDirectory, inputPathParentDirectory), $"{Path.GetFileNameWithoutExtension(inputPath)}.json");
    else
        destinationPath = Path.Combine(outputPath, $"{Path.GetFileNameWithoutExtension(inputPath)}.json");

    Console.WriteLine("Converting {0} into {1} ...", inputPath, destinationPath);
    await SingleResxConverter.ConvertFileAsync(inputPath, destinationPath);
    Console.WriteLine("Converted {0} into {1}", inputPath, destinationPath);
}