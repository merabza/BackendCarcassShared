using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BackendCarcassShared.Contracts.Errors;
using BackendCarcassShared.Contracts.V1.Requests;
using BackendCarcassShared.Contracts.V1.Responses;
using BackendCarcassShared.Contracts.V1.Routes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SystemTools.ReCounterContracts;
using SystemTools.SharedKernel;

namespace BackendCarcassShared.Contracts;

// გამოიყენება ProcessorWorkerConsole პროექტში
// ReSharper disable once UnusedType.Global
public /*open*/ class JwtContractReCounterApiClient : ReCounterApiClient
{
    private readonly ClientCredentialsTokenProvider? _tokenProvider;

    protected JwtContractReCounterApiClient(ILogger logger, IHttpClientFactory httpClientFactory, string server,
        bool useConsole) : base(logger, httpClientFactory, new ReCounterMessageHubClient(server, null), server, null,
        useConsole)
    {
    }

    //SSO რეჟიმი: კლიენტი sso.ltg.ge-ზე client_credentials-ით შედის, მომხმარებლის სახელისა და პაროლის გარეშე
    protected JwtContractReCounterApiClient(ILogger logger, IHttpClientFactory httpClientFactory, string server,
        bool useConsole, ClientCredentialsTokenProvider tokenProvider) : this(logger, httpClientFactory, server,
        useConsole)
    {
        _tokenProvider = tokenProvider;
    }

    //client_credentials-ის ტოკენის მიღება (ან ქეშიდან აღება) და მისი გამოყენება შემდეგ მოთხოვნებში.
    //ტოკენის ვადა მოკლეა, ამიტომ ეს ყოველი სამუშაოს დაწყებამდე გამოიძახება
    public async Task<Result> AuthenticateWithClientCredentials(CancellationToken cancellationToken = default)
    {
        if (_tokenProvider is null)
        {
            return Result.Failure(SsoErrors.TokenRequestFailed);
        }

        Result<string> token = await _tokenProvider.GetAccessTokenAsync(cancellationToken);
        if (token.IsFailure)
        {
            return Result.Failure(token.Error);
        }

        SetToken(token.Value);
        return Result.Success();
    }

    public Task<Result> IsCurrentUserValid(string token, CancellationToken cancellationToken = default)
    {
        return GetWithTokenAsync(token,
            CarcassApiRoutes.UserRights.UserRightsBase + CarcassApiRoutes.UserRights.IsCurrentUserValid,
            cancellationToken);
    }

    //ტანში პაროლია, ამიტომ შეცდომის პასუხისას (მაგალითად, არასწორი პაროლისას) ტანი კონსოლზე არ იბეჭდება
    public Task<Result<LoginResponse>> Login(LoginRequest loginRequest, CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<LoginResponse>(
            CarcassApiRoutes.Authentication.AuthenticationBase + CarcassApiRoutes.Authentication.Login, false,
            JsonConvert.SerializeObject(loginRequest), true, cancellationToken);
    }

    public void SetToken(string accessToken)
    {
        AccessToken = accessToken;
        (MessageHubClient as ReCounterMessageHubClient)?.SetToken(AccessToken);
    }
}
