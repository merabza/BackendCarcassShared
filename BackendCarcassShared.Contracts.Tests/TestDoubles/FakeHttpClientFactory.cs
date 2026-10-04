using System.Net.Http;

namespace BackendCarcassShared.Contracts.Tests.TestDoubles;

//ყველა კლიენტი ერთსა და იმავე handler-ს იყენებს
internal sealed class FakeHttpClientFactory : IHttpClientFactory
{
    private readonly HttpMessageHandler _handler;

    public FakeHttpClientFactory(HttpMessageHandler handler)
    {
        _handler = handler;
    }

    public HttpClient CreateClient(string name)
    {
        return new HttpClient(_handler, false);
    }
}
