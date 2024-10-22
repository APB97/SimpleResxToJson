using apb97.github.io.SimpleResxToJson.Shared;
using System.Collections.Immutable;

var inputs = args.Where(arg => arg.StartsWith("--input="))
    .Select(arg => arg.Replace("--input=", string.Empty))
    .ToImmutableArray();
var output = args.Where(arg => arg.StartsWith("--output="))
    .Select(arg => arg.Replace("--output=", string.Empty))
    .ToImmutableArray();

var silent = args.Any(arg => arg == "--silent");
var topDirectoryOnly = args.Any(arg => arg == "--top-dir-only");

if (inputs.Length > 0)
{
    string? outputPath = output.Length == 1 ? output[0] : null;
    await ProcessInputs(inputs, outputPath, silent, topDirectoryOnly);
}

static async Task ProcessInputs(ImmutableArray<string> inputs, string? outputPath, bool silent, bool topDirectoryOnly)
{
    var converter = new SingleResxConverter { Silent = silent };
    var multiConverter = new MultipleResxConverter(converter) { DirectorySearchOption = topDirectoryOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories };

    foreach (var inputPath in inputs)
    {
        if (Directory.Exists(inputPath))
        {
            await multiConverter.ProcessMultipleFiles(outputPath, inputPath);
        }
        else
        {
            await converter.ProcessSingleFile(outputPath, inputPath, Path.GetDirectoryName(inputPath));
        }
    }
}
