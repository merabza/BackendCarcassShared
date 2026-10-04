using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BackendCarcassShared.Contracts.Tests.TestDoubles;

//ლოკალური სერვერი თავისუფალ პორტზე: იღებს ერთ HTTP მოთხოვნას, იმახსოვრებს მის თავს (მოთხოვნის ხაზს და header-ებს)
//და 404-ით პასუხობს. შეტყობინებების hub-ი თავის მოთხოვნებს IHttpClientFactory-ის გარეშე აგზავნის, ამიტომ მისი
//მოთხოვნის სანახავად ნამდვილი სოკეტია საჭირო
internal sealed class OneRequestHttpServer : IDisposable
{
    private const string NotFoundResponse =
        "HTTP/1.1 404 Not Found\r\nContent-Length: 0\r\nConnection: close\r\n\r\n";

    private readonly TcpListener _listener = new(IPAddress.Loopback, 0);

    public OneRequestHttpServer()
    {
        _listener.Start();
        RequestHead = AcceptOneRequest();
    }

    public int Port => ((IPEndPoint)_listener.LocalEndpoint).Port;

    public Task<string> RequestHead { get; }

    public void Dispose()
    {
        _listener.Dispose();
    }

    private async Task<string> AcceptOneRequest()
    {
        using TcpClient client = await _listener.AcceptTcpClientAsync();
        NetworkStream stream = client.GetStream();
        var head = new StringBuilder();
        var buffer = new byte[1024];
        while (!head.ToString().Contains("\r\n\r\n", StringComparison.Ordinal))
        {
            int read = await stream.ReadAsync(buffer);
            if (read == 0)
            {
                break;
            }

            head.Append(Encoding.ASCII.GetString(buffer, 0, read));
        }

        await stream.WriteAsync(Encoding.ASCII.GetBytes(NotFoundResponse));
        return head.ToString();
    }
}
