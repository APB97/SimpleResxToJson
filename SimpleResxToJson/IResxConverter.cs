
namespace apb97.github.io.SimpleResxToJson.Shared
{
    public interface IResxConverter
    {
        Task WriteAsJsonToStreamAsync(Stream resxInputStream, Stream jsonOutput);
    }
}