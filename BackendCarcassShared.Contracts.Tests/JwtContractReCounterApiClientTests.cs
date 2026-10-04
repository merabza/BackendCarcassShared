using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BackendCarcassShared.Contracts.Tests.TestDoubles;
using BackendCarcassShared.Contracts.V1.Requests;
using BackendCarcassShared.Contracts.V1.Responses;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using SystemTools.SharedKernel;
using Xunit;

namespace BackendCarcassShared.Contracts.Tests;

//The tests of one class run one after another, so replacing Console.Out here is safe. The password is made up
public sealed class JwtContractReCounterApiClientTests
{
    private const string Server = "http://localhost:5050/api/v1";
    private const string MadeUpPassword = "made-up-password";

    private static TestableJwtContractReCounterApiClient CreateClient(HttpMessageHandler handler, bool useConsole,
        ILogger logger)
    {
        return new TestableJwtContractReCounterApiClient(logger, new FakeHttpClientFactory(handler), Server,
            useConsole);
    }

    private static LoginRequest MadeUpLogin()
    {
        return new LoginRequest { UserName = "made-up-user", Password = MadeUpPassword };
    }

    private static async Task<(T Result, string Output)> CaptureConsole<T>(Func<Task<T>> action)
    {
        TextWriter original = Console.Out;
        await using var writer = new StringWriter(CultureInfo.InvariantCulture);
        Console.SetOut(writer);
        try
        {
            T result = await action();
            return (result, writer.ToString());
        }
        finally
        {
            Console.SetOut(original);
        }
    }

    [Fact]
    public async Task IsCurrentUserValid_SendsTheTokenAsBearerToTheUserRightsRoute()
    {
        using var handler = new StubHttpMessageHandler(HttpStatusCode.OK, null);
        TestableJwtContractReCounterApiClient client = CreateClient(handler, false, Mock.Of<ILogger>());

        Result result = await client.IsCurrentUserValid("made-up-token");

        Assert.True(result.IsSuccess);
        Assert.Equal(HttpMethod.Get, handler.LastRequestMethod);
        Assert.Equal("/api/v1/userrights/iscurrentuservalid", handler.LastRequestUri!.AbsolutePath);
        Assert.Equal("Bearer made-up-token", handler.LastRequestAuthorization);
    }

    [Fact]
    public async Task IsCurrentUserValid_ReturnsTheFailure_WhenTheServerRefusesTheToken()
    {
        using var handler = new StubHttpMessageHandler(HttpStatusCode.Unauthorized, null);
        TestableJwtContractReCounterApiClient client = CreateClient(handler, false, Mock.Of<ILogger>());

        Result result = await client.IsCurrentUserValid("made-up-token");

        Assert.True(result.IsFailure);
    }

    //The token of SetToken goes with the requests that take the access token of the client, such as the status of the
    //current process
    [Fact]
    public async Task SetToken_SendsTheTokenAsBearerWithTheNextRequest()
    {
        using var handler = new StubHttpMessageHandler(HttpStatusCode.OK, "{}");
        TestableJwtContractReCounterApiClient client = CreateClient(handler, false, Mock.Of<ILogger>());

        client.SetToken("made-up-token");
        await client.GetCurrentProcessStatus();

        Assert.Equal(HttpMethod.Get, handler.LastRequestMethod);
        Assert.Equal("Bearer made-up-token", handler.LastRequestAuthorization);
    }

    //The message hub sends its own requests (not through IHttpClientFactory), so a local server catches the negotiation
    //of the hub; it answers 404, the hub gives up and the request itself goes on
    [Fact]
    public async Task SetToken_GivesTheTokenToTheMessageHub()
    {
        using var server = new OneRequestHttpServer();
        using var handler = new StubHttpMessageHandler(HttpStatusCode.OK, null);
        var client = new TestableJwtContractReCounterApiClient(Mock.Of<ILogger>(), new FakeHttpClientFactory(handler),
            $"http://127.0.0.1:{server.Port}/api/v1", false);

        client.SetToken("made-up-token");
        (Result result, _) = await CaptureConsole(() => client.PostWithMessageHub("/recounter/start").AsTask());
        string hubRequest = await server.RequestHead.WaitAsync(TimeSpan.FromSeconds(30));

        Assert.True(result.IsSuccess);
        Assert.StartsWith("POST /api/v1/recounter/messages/negotiate", hubRequest, StringComparison.Ordinal);
        Assert.Contains("Authorization: Bearer made-up-token", hubRequest, StringComparison.Ordinal);
    }

    //Logging in needs no progress messages. On port 0 a started hub would fail at once and write that to the console
    [Fact]
    public async Task Login_DoesNotStartTheMessageHub()
    {
        using var handler = new StubHttpMessageHandler(HttpStatusCode.OK, """{"userId":7}""");
        var client = new TestableJwtContractReCounterApiClient(Mock.Of<ILogger>(), new FakeHttpClientFactory(handler),
            "http://127.0.0.1:0/api/v1", false);

        (Result<LoginResponse> result, string output) = await CaptureConsole(() => client.Login(MadeUpLogin()));

        Assert.Equal(7, result.Value.UserId);
        Assert.Equal(string.Empty, output);
    }

    [Fact]
    public async Task GetCurrentProcessStatus_SendsNoAuthorization_WithoutAToken()
    {
        using var handler = new StubHttpMessageHandler(HttpStatusCode.OK, "{}");
        TestableJwtContractReCounterApiClient client = CreateClient(handler, false, Mock.Of<ILogger>());

        await client.GetCurrentProcessStatus();

        Assert.Equal(HttpMethod.Get, handler.LastRequestMethod);
        Assert.Null(handler.LastRequestAuthorization);
    }

    [Fact]
    public async Task Login_PostsTheUserAndThePasswordAndReturnsTheResponse()
    {
        using var handler = new StubHttpMessageHandler(HttpStatusCode.OK,
            """{"userId":7,"userName":"made-up-user","token":"made-up-token","appClaims":["claim"]}""");
        TestableJwtContractReCounterApiClient client = CreateClient(handler, false, Mock.Of<ILogger>());

        Result<LoginResponse> result = await client.Login(MadeUpLogin());

        Assert.Equal(7, result.Value.UserId);
        Assert.Equal("made-up-user", result.Value.UserName);
        Assert.Equal("made-up-token", result.Value.Token);
        Assert.Equal(["claim"], result.Value.AppClaims);
        Assert.Equal(HttpMethod.Post, handler.LastRequestMethod);
        Assert.Equal("/api/v1/authentication/login", handler.LastRequestUri!.AbsolutePath);
        LoginRequest sent = JsonConvert.DeserializeObject<LoginRequest>(handler.LastRequestBody!)!;
        Assert.Equal("made-up-user", sent.UserName);
        Assert.Equal(MadeUpPassword, sent.Password);
    }

    //A wrong password is the usual failure: the console shows the address and the status, but not the request body,
    //which holds the password, and the log gets only the answer of the server
    [Fact]
    public async Task Login_WritesThePasswordNeitherToTheConsoleNorToTheLog_WhenTheServerRefusesTheLogin()
    {
        using var handler = new StubHttpMessageHandler(HttpStatusCode.Unauthorized, null);
        var logger = new Mock<ILogger>();
        logger.Setup(l => l.IsEnabled(It.IsAny<LogLevel>())).Returns(true);
        TestableJwtContractReCounterApiClient client = CreateClient(handler, true, logger.Object);

        (Result<LoginResponse> result, string output) = await CaptureConsole(() => client.Login(MadeUpLogin()));

        Assert.True(result.IsFailure);
        Assert.Contains("[ERROR] answer after uri: POST http://localhost:5050/api/v1/authentication/login", output,
            StringComparison.Ordinal);
        Assert.Contains("401 Unauthorized", output, StringComparison.Ordinal);
        Assert.DoesNotContain("request body was", output, StringComparison.Ordinal);
        Assert.DoesNotContain(MadeUpPassword, output, StringComparison.Ordinal);
        Assert.Contains(logger.Invocations, i => i.Method.Name == nameof(ILogger.Log));
        Assert.DoesNotContain(logger.Invocations,
            i => i.Arguments.Any(a => a?.ToString()?.Contains(MadeUpPassword, StringComparison.Ordinal) == true));
    }
}
