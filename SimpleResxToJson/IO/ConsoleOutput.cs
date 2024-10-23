namespace apb97.github.io.SimpleResxToJson.Shared.IO;

/// <summary>
/// Console Output class for <see cref="IOutput"/> interface.
/// </summary>
public class ConsoleOutput : IOutput
{
    /// <summary>
    /// Forwads parameters to <see cref="Console.WriteLine(string, object?[]?)"/> for printing to console
    /// </summary>
    /// <param name="message"></param>
    /// <param name="parameters"></param>
    public void PrintMessage(string message, params object[] parameters)
    {
        Console.WriteLine(message, parameters);
    }
}
