namespace apb97.github.io.SimpleResxToJson.Shared;

public class SingleResxConverter
{
    public static async Task ProcessSingleFile(string? outputPath, string inputPath, string? inputDirectory)
    {
        string destinationPath = GetDestinationPath(outputPath, inputPath, inputDirectory);

        Console.WriteLine("Converting {0} into {1} ...", inputPath, destinationPath);
        await ConvertFileAsync(inputPath, destinationPath);
        Console.WriteLine("Converted {0} into {1}", inputPath, destinationPath);
    }

    public static string GetDestinationPath(string? outputPath, string inputPath, string? inputDirectory)
    {
        var inputPathParentDirectory = Path.GetDirectoryName(inputPath);
        return GetDestinationPath(outputPath ?? Directory.GetCurrentDirectory(), inputPath, inputDirectory, inputPathParentDirectory);
    }

    private static string GetDestinationPath(string outputPath, string inputPath, string? inputDirectory, string? inputPathParentDirectory)
    {
        if (inputDirectory != null && inputPathParentDirectory != null && inputDirectory != inputPathParentDirectory)
            return Path.Combine(outputPath, Path.GetRelativePath(inputDirectory, inputPathParentDirectory), GetTargetFileName(inputPath));
        else
            return Path.Combine(outputPath, GetTargetFileName(inputPath));
    }

    private static string GetTargetFileName(string inputPath)
    {
        return $"{Path.GetFileNameWithoutExtension(inputPath)}.json";
    }

    public static async Task ConvertFileAsync(string inputPath, string outputPath)
    {
        using var inputFile = File.OpenRead(inputPath);
        string? parentDirectory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(parentDirectory))
            Directory.CreateDirectory(parentDirectory);
        using var outputFile = File.Open(outputPath, FileMode.Create, FileAccess.Write);
        await ResxConverter.WriteAsJsonToStreamAsync(inputFile, outputFile);
    }
}
