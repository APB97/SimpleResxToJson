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
    foreach (var input in inputs)
    {
        var inputPath = input.Replace("--input=", string.Empty);
        outputPath ??= Path.Combine(Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory(), $"{Path.GetFileNameWithoutExtension(inputPath)}.json");
        Console.WriteLine("Converting {0} into {1} ...", inputPath, outputPath);
        await SingleResxConverter.ConvertFileAsync(inputPath, outputPath);
        Console.WriteLine("Converted {0} into {1}", inputPath, outputPath);
    }
}