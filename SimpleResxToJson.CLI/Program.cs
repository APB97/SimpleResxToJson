using apb97.github.io.SimpleResxToJson.Shared;
using apb97.github.io.SimpleResxToJson.Shared.IO;
using System.Collections.Immutable;

var inputs = args.Where(arg => arg.StartsWith("--input="))
    .Select(arg => arg.Replace("--input=", string.Empty))
    .ToImmutableArray();
var output = args.Where(arg => arg.StartsWith("--output="))
    .Select(arg => arg.Replace("--output=", string.Empty))
    .ToImmutableArray();

var silent = args.Any(arg => arg == "--silent");
var recursive = args.Any(arg => arg == "--recursive" || arg == "-R");

if (inputs.Length > 0)
{
    string? outputPath = output.Length == 1 ? output[0] : null;
    await ProcessInputs(inputs, outputPath, silent, recursive);
}

static async Task ProcessInputs(ImmutableArray<string> inputs, string? outputPath, bool silent, bool recursive)
{
    IOutput messageOutput = silent ? NullOutput.Instance : new ConsoleOutput();
    var converter = new SingleResxConverter(messageOutput);
    var multiConverter = new MultipleResxConverter(converter) { DirectorySearchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly };

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
