using apb97.github.io.SimpleResxToJson.Shared.IO;

namespace apb97.github.io.SimpleResxToJson.Shared;

/// <summary>
/// Creates a ResX converter accepting one file at a time
/// </summary>
/// <param name="messsageOutputTarget">Target for output messages.</param>
public sealed class SingleResxConverter(IOutput messsageOutputTarget, IResxConverter converter)
{
    public SingleResxConverter(ResxConverterOptions options)
        : this(options.Silent ? NullOutput.Instance : ConsoleOutput.Instance,
              options.StringDataOnly ? new StringsOnlyResxConverter(options) : new AllDataResxConverter(options)) { }

    /// <summary>
    /// Converts single *.resx file into *.json file. When <paramref name="outputDirectory"/> is omitted, uses application's current working directory.
    /// Outputs message to <see cref="IOutput"/> (<seealso cref="NullOutput"/> or <seealso cref="ConsoleOutput"/>) when starting and finishing conversion.
    /// </summary>
    /// <param name="outputDirectory">Directory in which all conversion results should be placed.</param>
    /// <param name="inputFile">*.resx file that should be converted.</param>
    /// <param name="inputDirectory">Directory that is used to calculate relative path from outputDirectory where the final result will be created.</param>
    /// <returns></returns>
    public async Task ProcessSingleFileAsync(string? outputDirectory, string inputFile, string? inputDirectory)
    {
        string destinationFile = GetDestinationPath(outputDirectory, inputFile, inputDirectory);

        messsageOutputTarget.PrintMessage("Converting {0} into {1} ...", inputFile, destinationFile);
        await ConvertFileAsync(inputFile, destinationFile);
        messsageOutputTarget.PrintMessage("Converted {0} into {1}", inputFile, destinationFile);
    }

    /// <summary>
    /// Determines output file location when conversion happens.
    /// </summary>
    /// <param name="outputDirectory">Directory in which all conversion results should be placed.</param>
    /// <param name="inputFile">*.resx file that should be checked for output file path.</param>
    /// <param name="inputDirectory">Directory that is used to calculate relative path from outputDirectory where the final result will be created.</param>
    /// <returns>Path to file that will be recreated (created or truncated) when <see cref="ProcessSingleFileAsync(string?, string, string?)"/> is called.</returns>
    public static string GetDestinationPath(string? outputDirectory, string inputFile, string? inputDirectory)
    {
        return GetDestinationPath(outputDirectory ?? Directory.GetCurrentDirectory(), inputFile, inputDirectory, Path.GetDirectoryName(inputFile));
    }

    private static string GetDestinationPath(string outputPath, string inputFile, string? inputDirectory, string? inputPathParentDirectory)
    {
        if (inputDirectory != null && inputPathParentDirectory != null && inputDirectory != inputPathParentDirectory)
            return Path.Combine(outputPath, Path.GetRelativePath(inputDirectory, inputPathParentDirectory), GetTargetFileName(inputFile));
        else
            return Path.Combine(outputPath, GetTargetFileName(inputFile));
    }

    private static string GetTargetFileName(string inputFile)
    {
        return $"{Path.GetFileNameWithoutExtension(inputFile)}.json";
    }

    /// <summary>
    /// Alternative API for converting a file from any <paramref name="inputPath"/> to any <paramref name="outputPath"/>
    /// </summary>
    /// <remarks>Note that expected input should be in correct format</remarks>
    /// <param name="inputPath">File to be converted</param>
    /// <param name="outputPath">File to be recreated (created or truncated) as a result of conversion.</param>
    public async Task ConvertFileAsync(string inputPath, string outputPath)
    {
        using var inputFile = File.OpenRead(inputPath);
        string? parentDirectory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(parentDirectory))
            Directory.CreateDirectory(parentDirectory);
        using var outputFile = File.Open(outputPath, FileMode.Create, FileAccess.Write);
        await converter.WriteAsJsonToStreamAsync(inputFile, outputFile);
    }
}
