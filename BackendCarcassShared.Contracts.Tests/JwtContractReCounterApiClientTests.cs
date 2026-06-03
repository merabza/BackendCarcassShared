using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BackendCarcassShared.Contracts.V1.Requests;
using BackendCarcassShared.Contracts.V1.Responses;
using BackendCarcassShared.Contracts.V1.Routes;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SystemTools.SystemToolsShared.Errors;
using Xunit;

namespace BackendCarcassShared.Contracts.Tests;

public sealed class JwtContractReCounterApiClientTests : IDisposable
{
    private const string TestServer = "http://localhost:5000";
    private const string TestToken = "test-jwt-token";
    private readonly HttpClient _httpClient;
    private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly Mock<ILogger<JwtContractReCounterApiClient>> _mockLogger;

    public JwtContractReCounterApiClientTests()
    {
        _mockLogger = new Mock<ILogger<JwtContractReCounterApiClient>>();
        _mockHttpClientFactory = new Mock<IHttpClientFactory>();
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

        _httpClient = new HttpClient(_mockHttpMessageHandler.Object) { BaseAddress = new Uri(TestServer) };

        _mockHttpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(_httpClient);
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }

    [Fact]
    public void Constructor_WithValidParameters_CreatesInstance()
    {
        // Arrange & Act
        var client = new TestableJwtContractReCounterApiClient(
            _mockLogger.Object, _mockHttpClientFactory.Object, TestServer, false);

        // Assert
        Assert.NotNull(client);
    }

    [Fact]
    public async Task IsCurrentUserValid_WithValidToken_ReturnsNone()
    {
        // Arrange
        var client = new TestableJwtContractReCounterApiClient(
            _mockLogger.Object, _mockHttpClientFactory.Object, TestServer, false);

        using var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK, Content = new StringContent("")
        };

        _mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get && req.RequestUri != null &&
                    req.RequestUri.ToString().Contains(CarcassApiRoutes.UserRights.IsCurrentUserValid) &&
                    req.Headers.Authorization != null && req.Headers.Authorization.Scheme == "Bearer" &&
                    req.Headers.Authorization.Parameter == TestToken), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act
        var result = await client.IsCurrentUserValid(TestToken);

        // Assert
        Assert.True(result.IsNone);
    }

    [Fact]
    public async Task IsCurrentUserValid_WithInvalidToken_ReturnsErrors()
    {
        // Arrange
        var client = new TestableJwtContractReCounterApiClient(
            _mockLogger.Object, _mockHttpClientFactory.Object, TestServer, false);

        var errors = new[] { new Error { Code = "NotFound", Name = "Invalid token" } };
        string errorJson = JsonConvert.SerializeObject(errors);

        using var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent(errorJson)
        };

        _mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get && req.RequestUri != null && req.RequestUri.ToString()
                        .Contains(CarcassApiRoutes.UserRights.IsCurrentUserValid)), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act
        var result = await client.IsCurrentUserValid(TestToken);

        // Assert
        Assert.True(result.IsSome);
        var errorList = result.Match(errs => errs, () => Array.Empty<Error>());
        Assert.NotEmpty(errorList);
    }

    [Fact]
    public async Task IsCurrentUserValid_WithCancellation_ThrowsTaskCanceledException()
    {
        // Arrange
        var client = new TestableJwtContractReCounterApiClient(
            _mockLogger.Object, _mockHttpClientFactory.Object, TestServer, false);

        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()).ThrowsAsync(new TaskCanceledException());

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(() => client.IsCurrentUserValid(TestToken, cts.Token));
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsLoginResponse()
    {
        // Arrange
        var client = new TestableJwtContractReCounterApiClient(
            _mockLogger.Object, _mockHttpClientFactory.Object, TestServer, false);

        var loginRequest = new LoginRequest { UserName = "testuser", Password = "testpassword" };

        var expectedResponse = new LoginResponse(
            1, "testuser", "test@example.com", "jwt-token", "Test", "User", "Admin");

        string responseJson = JsonConvert.SerializeObject(expectedResponse);

        using var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK, Content = new StringContent(responseJson)
        };

        _mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Post && req.RequestUri != null &&
                req.RequestUri.ToString().Contains(CarcassApiRoutes.Authentication.Login)),
            ItExpr.IsAny<CancellationToken>()).ReturnsAsync(response);

        // Act
        var result = await client.Login(loginRequest);

        // Assert
        Assert.True(result.IsT0);
        result.Switch(loginResponse =>
        {
            Assert.Equal("testuser", loginResponse.UserName);
            Assert.Equal("test@example.com", loginResponse.Email);
            Assert.Equal("jwt-token", loginResponse.Token);
            Assert.Equal(1, loginResponse.UserId);
        }, errors => Assert.Fail("Expected LoginResponse but got errors"));
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsErrors()
    {
        // Arrange
        var client = new TestableJwtContractReCounterApiClient(
            _mockLogger.Object, _mockHttpClientFactory.Object, TestServer, false);

        var loginRequest = new LoginRequest { UserName = "invaliduser", Password = "wrongpassword" };

        var errors = new[] { new Error { Code = "Unauthorized", Name = "Invalid credentials" } };
        string errorJson = JsonConvert.SerializeObject(errors);

        using var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent(errorJson)
        };

        _mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Post && req.RequestUri != null &&
                req.RequestUri.ToString().Contains(CarcassApiRoutes.Authentication.Login)),
            ItExpr.IsAny<CancellationToken>()).ReturnsAsync(response);

        // Act
        var result = await client.Login(loginRequest);

        // Assert
        Assert.True(result.IsT1);
        result.Switch(loginResponse => Assert.Fail("Expected errors but got LoginResponse"),
            errors => Assert.NotEmpty(errors));
    }

    [Fact]
    public async Task Login_WithCancellation_ThrowsTaskCanceledException()
    {
        // Arrange
        var client = new TestableJwtContractReCounterApiClient(
            _mockLogger.Object, _mockHttpClientFactory.Object, TestServer, false);

        var loginRequest = new LoginRequest { UserName = "testuser", Password = "testpassword" };

        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()).ThrowsAsync(new TaskCanceledException());

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(() => client.Login(loginRequest, cts.Token));
    }

    [Fact]
    public void SetToken_WithValidToken_UpdatesAccessToken()
    {
        // Arrange
        var client = new TestableJwtContractReCounterApiClient(
            _mockLogger.Object, _mockHttpClientFactory.Object, TestServer, false);

        const string newToken = "new-jwt-token";

        // Act
        client.SetToken(newToken);

        // Assert
        Assert.Equal(newToken, client.GetAccessToken());
    }

    [Fact]
    public void SetToken_WithNullToken_SetsAccessTokenToNull()
    {
        // Arrange
        var client = new TestableJwtContractReCounterApiClient(
            _mockLogger.Object, _mockHttpClientFactory.Object, TestServer, false);

        client.SetToken("initial-token");

        // Act
        client.SetToken(null!);

        // Assert
        Assert.Null(client.GetAccessToken());
    }

    [Fact]
    public void SetToken_CalledMultipleTimes_UpdatesTokenEachTime()
    {
        // Arrange
        var client = new TestableJwtContractReCounterApiClient(
            _mockLogger.Object, _mockHttpClientFactory.Object, TestServer, false);

        const string firstToken = "first-jwt-token";
        const string secondToken = "second-jwt-token";

        // Act & Assert
        client.SetToken(firstToken);
        Assert.Equal(firstToken, client.GetAccessToken());

        client.SetToken(secondToken);
        Assert.Equal(secondToken, client.GetAccessToken());
    }

    [Fact]
    public async Task Login_SerializesRequestCorrectly()
    {
        // Arrange
        var client = new TestableJwtContractReCounterApiClient(
            _mockLogger.Object, _mockHttpClientFactory.Object, TestServer, false);

        var loginRequest = new LoginRequest { UserName = "testuser", Password = "testpassword" };

        string? capturedContent = null;

        using var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(new LoginResponse(1, "testuser",
                "test@example.com", "token", "Test", "User", "Admin")))
        };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post), ItExpr.IsAny<CancellationToken>())
            .Callback<HttpRequestMessage, CancellationToken>(async (req, ct) =>
            {
                if (req.Content != null)
                {
                    capturedContent = await req.Content.ReadAsStringAsync(ct);
                }
            }).ReturnsAsync(response);

        // Act
        await client.Login(loginRequest);

        // Assert
        Assert.NotNull(capturedContent);
        var deserializedRequest = JsonConvert.DeserializeObject<LoginRequest>(capturedContent!);
        Assert.NotNull(deserializedRequest);
        Assert.Equal(loginRequest.UserName, deserializedRequest!.UserName);
        Assert.Equal(loginRequest.Password, deserializedRequest.Password);
    }

    // Testable wrapper class to expose protected members
    private sealed class TestableJwtContractReCounterApiClient : JwtContractReCounterApiClient
    {
        public TestableJwtContractReCounterApiClient(ILogger logger, IHttpClientFactory httpClientFactory,
            string server, bool useConsole) : base(logger, httpClientFactory, server, useConsole)
        {
        }

        public string? GetAccessToken()
        {
            return AccessToken;
        }
    }
}
