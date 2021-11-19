using System.Net.Sockets;
using System.Text.Json;
using CommandLine;
using Mess.Server;

AppArguments parsedArgs = null;
Parser.Default.ParseArguments<AppArguments>(args).WithParsed(parsed => parsedArgs = parsed);

if(parsedArgs == null)
    Environment.Exit(1);

Console.WriteLine(JsonSerializer.Serialize(parsedArgs));

var listener = TcpListener.Create(parsedArgs.ListeningPort);
listener.Start(0xff);

var clients = new List<TcpClient>();

while(true)
{
    if(listener.Pending())
        clients.Add(await listener.AcceptTcpClientAsync());

    foreach(TcpClient client in clients)
    {
        if(client.Available > 0)
        {
            byte[] data = new byte[client.Available];
            client.GetStream().Read(data);

            clients.AsParallel().ForAll(cl =>
            {
                if(cl == client)
                    return;

                cl.GetStream().Write(data);
            });
        }
    }

    await Task.Delay(1000);
}