namespace apb97.github.io.SimpleResxToJson.Shared.IO;

public sealed class NullOutput : IOutput
{
    private NullOutput() { }

    /// <summary>
    /// As it is a NullOutput method, it simply does nothing
    /// </summary>
    public void PrintMessage(string message, params object[] parameters) { }

    public static readonly NullOutput Instance = new();
}
