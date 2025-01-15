using apb97.github.io.SimpleResxToJson.Shared;
using System.Collections.Immutable;

var inputs = args.Where(arg => arg.StartsWith("--input="))
    .Select(arg => arg.Replace("--input=", string.Empty))
    .ToImmutableArray();
var output = args.Where(arg => arg.StartsWith("--output="))
    .Select(arg => arg.Replace("--output=", string.Empty))
    .ToImmutableArray();

var silent = args.Any(arg => arg == "--silent");
var recursive = args.Any(arg => arg == "--recursive" || arg == "-R");
var includeAllTypes = args.Any(arg => arg == "--include-all");
var noTypeParsing = args.Any(arg => arg == "--no-type-parsing");

if (inputs.Length > 0)
{
    string? outputPath = output.Length == 1 ? output[0] : null;
    var options = new ResxConverterOptions
    {
        Silent = silent,
        Recursive = recursive,
        StringDataOnly = !includeAllTypes,
        SkipNonStrings = !noTypeParsing,
    };
    await ProcessInputsAsync(inputs, outputPath, options);
}

static async Task ProcessInputsAsync(ImmutableArray<string> inputs, string? outputPath, ResxConverterOptions options)
{
    var multiConverter = new MultipleResxConverter(options);
    foreach (var inputPath in inputs)
    {
        await multiConverter.ProcessAsync(inputPath, outputPath);
    }
}
