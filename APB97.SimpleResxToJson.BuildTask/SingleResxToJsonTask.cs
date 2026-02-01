using apb97.github.io.SimpleResxToJson.Shared;
using System.ComponentModel.DataAnnotations;

namespace APB97.SimpleResxToJson.BuildTask;

public class SingleResxToJsonTask : Microsoft.Build.Utilities.Task
{
    public bool Silent { get; set; } = false;

    public bool StringsOnly { get; set; } = true;

    public bool SkipNonStrings { get; set; } = true;

    [Required]
    public required string Input { get; set; }

    [Required]
    public required string Output { get; set; }

    public override bool Execute()
    {
        var options = new ResxConverterOptions
        {
            Silent = Silent,
            Recursive = false,
            StringDataOnly = StringsOnly,
            SkipNonStrings = SkipNonStrings,
        };
        var singleConverter = new SingleResxConverter(options);
        singleConverter.ProcessSingleFileAsync(Output, Input, Path.GetDirectoryName(Input)).GetAwaiter().GetResult();
        return true;
    }
}
