using System.Net;
using System.Net.Sockets;

IPEndPoint ip;
do
{
    Console.Write("Server Address: ");
} while(!IPEndPoint.TryParse(Console.ReadLine() ?? "", out ip!));

using var client = new TcpClient();
await client.ConnectAsync(ip);

while(true)
{
    string msg = Console.ReadLine()!;

    if(client.Available > 0)
    {
        byte[] data = new byte[client.Available];
        client.GetStream().Read(data);
        Console.WriteLine(new string(data.Select(d => (char)d).ToArray()));
    }

    client.GetStream().Write(msg.Select(s => (byte)s).ToArray());
}