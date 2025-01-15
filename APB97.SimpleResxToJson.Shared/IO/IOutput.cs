namespace apb97.github.io.SimpleResxToJson.Shared.IO;

public interface IOutput
{
    void PrintMessage(string message, params object[] parameters);
}
