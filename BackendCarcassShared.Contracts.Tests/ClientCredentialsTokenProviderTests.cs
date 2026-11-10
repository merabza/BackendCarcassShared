using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BackendCarcassShared.Contracts.Tests.TestDoubles;
using Microsoft.Extensions.Logging;
using Moq;
using SystemTools.SharedKernel;
using Xunit;

namespace BackendCarcassShared.Contracts.Tests;

//The client secret is made up
public sealed class ClientCredentialsTokenProviderTests
{
    private const string Authority = "http://localhost:5090/";
    private const string MadeUpSecret = "made-up-secret";

    private static ClientCredentialsTokenProvider CreateProvider(HttpMessageHandler handler,
        TimeProvider? timeProvider = null)
    {
        return new ClientCredentialsTokenProvider(Mock.Of<ILogger>(), new FakeHttpClientFactory(handler), Authority,
            "grammar-worker", MadeUpSecret, "api.grammar", timeProvider);
    }

    [Fact]
    public async Task GetAccessTokenAsync_PostsTheClientCredentialsToTheTokenEndpoint()
    {
        using var handler = new StubHttpMessageHandler(HttpStatusCode.OK,
            """{"access_token":"made-up-token","expires_in":600,"token_type":"Bearer"}""");
        using ClientCredentialsTokenProvider provider = CreateProvider(handler);

        Result<string> token = await provider.GetAccessTokenAsync();

        Assert.Equal("made-up-token", token.Value);
        Assert.Equal(HttpMethod.Post, handler.LastRequestMethod);
        Assert.Equal("http://localhost:5090/connect/token", handler.LastRequestUri!.ToString());
        Assert.Contains("grant_type=client_credentials", handler.LastRequestBody, StringComparison.Ordinal);
        Assert.Contains("client_id=grammar-worker", handler.LastRequestBody, StringComparison.Ordinal);
        Assert.Contains("client_secret=" + MadeUpSecret, handler.LastRequestBody, StringComparison.Ordinal);
        Assert.Contains("scope=api.grammar", handler.LastRequestBody, StringComparison.Ordinal);
    }

    [Fact]
    public async Task GetAccessTokenAsync_ReusesTheToken_UntilSixtySecondsBeforeItsExpiry()
    {
        using var handler = new StubHttpMessageHandler(HttpStatusCode.OK,
            """{"access_token":"made-up-token","expires_in":600}""");
        var time = new ManualTimeProvider(new DateTimeOffset(2026, 11, 10, 12, 0, 0, TimeSpan.Zero));
        using ClientCredentialsTokenProvider provider = CreateProvider(handler, time);

        await provider.GetAccessTokenAsync();
        time.Advance(TimeSpan.FromSeconds(539));
        await provider.GetAccessTokenAsync();
        Assert.Equal(1, handler.RequestCount);

        time.Advance(TimeSpan.FromSeconds(2));
        await provider.GetAccessTokenAsync();
        Assert.Equal(2, handler.RequestCount);
    }

    [Fact]
    public async Task GetAccessTokenAsync_Fails_WhenTheServerRefusesTheClient()
    {
        using var handler = new StubHttpMessageHandler(HttpStatusCode.BadRequest, """{"error":"invalid_client"}""");
        using ClientCredentialsTokenProvider provider = CreateProvider(handler);

        Result<string> token = await provider.GetAccessTokenAsync();

        Assert.True(token.IsFailure);
    }

    [Fact]
    public async Task AuthenticateWithClientCredentials_SendsTheTokenAsBearerWithTheNextRequest()
    {
        using var tokenHandler = new StubHttpMessageHandler(HttpStatusCode.OK,
            """{"access_token":"made-up-token","expires_in":600}""");
        using var apiHandler = new StubHttpMessageHandler(HttpStatusCode.OK, "{}");
        using ClientCredentialsTokenProvider provider = CreateProvider(tokenHandler);
        var client = new TestableJwtContractReCounterApiClient(Mock.Of<ILogger>(),
            new FakeHttpClientFactory(apiHandler), "http://localhost:5022/api/v1", provider);

        Result result = await client.AuthenticateWithClientCredentials();
        await client.GetCurrentProcessStatus();

        Assert.True(result.IsSuccess);
        Assert.Equal("Bearer made-up-token", apiHandler.LastRequestAuthorization);
    }

    [Fact]
    public async Task AuthenticateWithClientCredentials_Fails_WithoutATokenProvider()
    {
        using var handler = new StubHttpMessageHandler(HttpStatusCode.OK, "{}");
        var client = new TestableJwtContractReCounterApiClient(Mock.Of<ILogger>(), new FakeHttpClientFactory(handler),
            "http://localhost:5022/api/v1", false);

        Result result = await client.AuthenticateWithClientCredentials();

        Assert.True(result.IsFailure);
    }
}
