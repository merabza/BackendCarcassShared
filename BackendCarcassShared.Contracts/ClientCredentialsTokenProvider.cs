using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using BackendCarcassShared.Contracts.Errors;
using Microsoft.Extensions.Logging;
using SystemTools.SharedKernel;

namespace BackendCarcassShared.Contracts;

//მანქანური კლიენტის (მაგალითად ProcessorWorkerConsole) წვდომის ტოკენი sso.ltg.ge-იდან client_credentials-ით.
//ტოკენი ინახება და ხელახლა მოითხოვება მხოლოდ მაშინ, როცა ვადის გასვლამდე 60 წამზე ნაკლები რჩება
public sealed class ClientCredentialsTokenProvider : IDisposable
{
    public const string HttpClientName = nameof(ClientCredentialsTokenProvider);

    //OpenID Connect-ის token endpoint-ი Authority-ის მიმართ (sso-design.md, D2)
    private const string TokenEndpointPath = "connect/token";

    private static readonly TimeSpan RefreshBeforeExpiry = TimeSpan.FromSeconds(60);

    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger _logger;
    private readonly SemaphoreSlim _lock = new(1, 1);
    private readonly string _scope;
    private readonly TimeProvider _timeProvider;
    private readonly Uri _tokenEndpoint;

    private string? _accessToken;
    private DateTimeOffset _expiresAt;

    public ClientCredentialsTokenProvider(ILogger logger, IHttpClientFactory httpClientFactory, string authority,
        string clientId, string clientSecret, string scope, TimeProvider? timeProvider = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(authority);
        ArgumentException.ThrowIfNullOrWhiteSpace(clientId);
        ArgumentException.ThrowIfNullOrWhiteSpace(clientSecret);
        ArgumentException.ThrowIfNullOrWhiteSpace(scope);
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _tokenEndpoint = new Uri(new Uri(authority), TokenEndpointPath);
        _clientId = clientId;
        _clientSecret = clientSecret;
        _scope = scope;
        _timeProvider = timeProvider ?? TimeProvider.System;
    }

    public void Dispose()
    {
        _lock.Dispose();
    }

    public async Task<Result<string>> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            if (_accessToken is not null && _timeProvider.GetUtcNow() < _expiresAt - RefreshBeforeExpiry)
            {
                return _accessToken;
            }

            return await RequestTokenAsync(cancellationToken);
        }
        finally
        {
            _lock.Release();
        }
    }

    private async Task<Result<string>> RequestTokenAsync(CancellationToken cancellationToken)
    {
        using HttpClient client = _httpClientFactory.CreateClient(HttpClientName);
        using var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "client_credentials",
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["scope"] = _scope
        });

        try
        {
            using HttpResponseMessage response = await client.PostAsync(_tokenEndpoint, content, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                //პასუხის ტანი საიდუმლოს არ შეიცავს, მაგრამ ლოგში მხოლოდ სტატუსი იწერება
                _logger.LogError("client_credentials token request failed with status {StatusCode}",
                    (int)response.StatusCode);
                return Result.Failure<string>(SsoErrors.TokenRequestFailed);
            }

            TokenResponse? token = await JsonSerializer.DeserializeAsync<TokenResponse>(
                await response.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken);
            if (string.IsNullOrWhiteSpace(token?.AccessToken))
            {
                return Result.Failure<string>(SsoErrors.TokenRequestFailed);
            }

            _accessToken = token.AccessToken;
            _expiresAt = _timeProvider.GetUtcNow().AddSeconds(token.ExpiresIn);
            return _accessToken;
        }
        catch (HttpRequestException e)
        {
            _logger.LogError(e, "client_credentials token request failed");
            return Result.Failure<string>(SsoErrors.TokenRequestFailed);
        }
    }

    private sealed class TokenResponse
    {
        [JsonPropertyName("access_token")] public string? AccessToken { get; init; }

        [JsonPropertyName("expires_in")] public int ExpiresIn { get; init; }
    }
}
