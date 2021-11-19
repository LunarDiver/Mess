using System.Text.Json;
using CommandLine;
using Mess.Server;

AppArguments parsedArgs = null;
Parser.Default.ParseArguments<AppArguments>(args).WithParsed(parsed => parsedArgs = parsed);

Console.WriteLine(JsonSerializer.Serialize(parsedArgs));