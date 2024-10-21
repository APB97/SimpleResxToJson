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
            await SingleResxConverter.ProcessSingleFile(outputPath, inputPath, Path.GetDirectoryName(inputPath));
        }
    }
}

static async Task ProcessMultipleFiles(string? outputPath, string inputPath)
{
    foreach (var file in Directory.EnumerateFiles(inputPath, "*.resx", SearchOption.AllDirectories))
    {
        await SingleResxConverter.ProcessSingleFile(outputPath, file, inputPath);
    }
}
