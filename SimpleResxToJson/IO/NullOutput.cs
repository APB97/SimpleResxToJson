namespace apb97.github.io.SimpleResxToJson.Shared.IO;

/// <summary>
/// Null Object pattern for <see cref="IOutput"/> interface.
/// </summary>
public sealed class NullOutput : IOutput
{
    private NullOutput() { }

    /// <summary>
    /// As it is a <see cref="NullOutput"/> method, it simply does nothing and ignores all parameters.
    /// </summary>
    public void PrintMessage(string message, params object[] parameters) { }

    /// <summary>
    /// An instance of <see cref="NullOutput"/>.
    /// </summary>
    public static readonly NullOutput Instance = new();
}
