namespace Mess.Server;

public class AppArguments
{
    [CommandLine.Option('p', "port", HelpText = "Specifies the port the server will listen to.")]
    public ushort ListeningPort { get; set; } = 3808;
}
