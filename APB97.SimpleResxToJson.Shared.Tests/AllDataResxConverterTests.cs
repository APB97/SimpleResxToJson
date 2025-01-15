using apb97.github.io.SimpleResxToJson.Shared;
using FluentAssertions;
using Xunit.Abstractions;

namespace APB97.SimpleResxToJson.Shared.Tests;

public class AllDataResxConverterTests
{
    [Theory]
    [MemberData(nameof(GetFileInfoTestData))]
    public void GetFileInfoTest(string value, string? comment, SerializableResxFileInfo? expected)
    {
        AllDataResxConverter.GetFileInfo(value, comment)
            .Should()
            .BeEquivalentTo(expected);
    }

    public static TheoryData<string, string?, SerializableResxFileInfo?> GetFileInfoTestData()
    {
        return new TheoryData<string, string?, SerializableResxFileInfo?>
        {
            { "my-picture.png;System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", null,
                new SerializableResxFileInfo
                {
                    Value = "my-picture.png",
                    Type = "System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                }
            },
            { "my-picture.png;System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", "TestPicture",
                new SerializableResxFileInfo
                { 
                    Value = "my-picture.png",
                    Type = "System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
                    Comment = "TestPicture"
                }
            },
            { "Test/my-picture.png;System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", null,
                new SerializableResxFileInfo
                {
                    Value = "Test/my-picture.png",
                    Type = "System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                }
            },
            { "Test/my-picture.png;System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", "TestPicture",
                new SerializableResxFileInfo
                {
                    Value = "Test/my-picture.png",
                    Type = "System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
                    Comment = "TestPicture"
                }
            },
            {
                @"Resources\somefile;System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;utf-8", null,
                new SerializableResxFileInfo
                {
                    Value = @"Resources\somefile",
                    Type = "System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
                    Encoding = "utf-8"
                }
            },
            {
                @"Resources\somefile;System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;utf-8", "TestFile",
                new SerializableResxFileInfo
                {
                    Value = @"Resources\somefile",
                    Type = "System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
                    Encoding = "utf-8",
                    Comment = "TestFile"
                }
            }
        };
    }
}

/// <summary>
/// for: xUnit1045
/// Avoid using TheoryData type arguments that might not be serializable
/// </summary>
public record SerializableResxFileInfo : ResxFileInfo, IXunitSerializable
{
    public void Deserialize(IXunitSerializationInfo info)
    {
        Value = info.GetValue<string>(nameof(Value));
        Type = info.GetValue<string>(nameof(Type));
        Encoding = info.GetValue<string>(nameof(Encoding));
        Comment = info.GetValue<string>(nameof(Comment));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(Value), Value);
        info.AddValue(nameof(Type), Type);
        info.AddValue(nameof(Encoding), Encoding);
        info.AddValue(nameof(Comment), Comment);
    }
}
