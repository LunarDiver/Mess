namespace Mess.Server;

public class AppArguments
{
    [CommandLine.Option('p', "port", Required = true, HelpText = "Specifies the port the server will listen to.")]
    public ushort ListeningPort { get; set; }
}
