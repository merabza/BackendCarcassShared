using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackendCarcassShared.Contracts.Tests.TestDoubles;

//იმახსოვრებს ბოლო მოთხოვნას და აბრუნებს წინასწარ მოცემულ პასუხს
internal sealed class StubHttpMessageHandler : HttpMessageHandler
{
    private readonly string? _body;
    private readonly HttpStatusCode _statusCode;

    public StubHttpMessageHandler(HttpStatusCode statusCode, string? body)
    {
        _statusCode = statusCode;
        _body = body;
    }

    public Uri? LastRequestUri { get; private set; }
    public HttpMethod? LastRequestMethod { get; private set; }
    public string? LastRequestBody { get; private set; }
    public string? LastRequestAuthorization { get; private set; }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        LastRequestUri = request.RequestUri;
        LastRequestMethod = request.Method;
        LastRequestAuthorization = request.Headers.Authorization?.ToString();
        LastRequestBody = request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken);

        var response = new HttpResponseMessage(_statusCode) { RequestMessage = request };
        if (_body is not null)
        {
            response.Content = new StringContent(_body, Encoding.UTF8, "application/json");
        }

        return response;
    }
}
