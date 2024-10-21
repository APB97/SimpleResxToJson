using apb97.github.io.SimpleResxToJson.Shared;
using FluentAssertions;

namespace SimpleResxToJson.Shared.Tests;

public class SingleResxConverterTests
{
    private static readonly char separator = Path.DirectorySeparatorChar;

    [Theory]
    [MemberData(nameof(GetDestinationPathTestData))]
    public void GetDestinationPathTest(string outputPath, string inputFile, string? inputDirectory, string expectedResult)
    {
        SingleResxConverter.GetDestinationPath(outputPath, inputFile, inputDirectory)
            .Should()
            .Be(expectedResult);
    }

    public static TheoryData<string, string, string?, string> GetDestinationPathTestData()
    {
        return new TheoryData<string, string, string?, string>
        { 
            { "Output", "Input/input.resx", "Input", $"Output{separator}input.json" },
            { "Output", "path/to/Data/input.resx", "path/to", $"Output{separator}Data{separator}input.json" },
            { $"path{separator}to{separator}Output", "path/to/Data/input.resx", "path/to", $"path{separator}to{separator}Output{separator}Data{separator}input.json" },
            { "Output", "input.en-US.resx", null, $"Output{separator}input.en-US.json" },
        };
    }
}