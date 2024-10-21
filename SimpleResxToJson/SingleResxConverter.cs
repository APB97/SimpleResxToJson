namespace apb97.github.io.SimpleResxToJson.Shared;

public class SingleResxConverter
{
    public static async Task ConvertFileAsync(string inputPath, string outputPath)
    {
        using var inputFile = File.OpenRead(inputPath);
        string? parentDirectory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(parentDirectory))
            Directory.CreateDirectory(parentDirectory);
        using var outputFile = File.OpenWrite(outputPath);
        await ResxConverter.WriteAsJsonToStreamAsync(inputFile, outputFile);
    }
}
