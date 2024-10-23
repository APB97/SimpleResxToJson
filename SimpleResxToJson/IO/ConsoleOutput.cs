namespace apb97.github.io.SimpleResxToJson.Shared.IO;

public class ConsoleOutput : IOutput
{
    public void PrintMessage(string message, params object[] parameters)
    {
        Console.WriteLine(message, parameters);
    }
}
