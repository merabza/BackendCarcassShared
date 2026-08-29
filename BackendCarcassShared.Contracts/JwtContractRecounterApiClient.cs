using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BackendCarcassShared.Contracts.V1.Requests;
using BackendCarcassShared.Contracts.V1.Responses;
using BackendCarcassShared.Contracts.V1.Routes;
using LanguageExt;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SystemTools.ReCounterContracts;
using SystemTools.SharedKernel;
using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts;

// გამოიყენება ProcessorWorkerConsole პროექტში
// ReSharper disable once UnusedType.Global
public /*open*/ class JwtContractReCounterApiClient : ReCounterApiClient
{
    // ReSharper disable once ConvertToPrimaryConstructor
    protected JwtContractReCounterApiClient(ILogger logger, IHttpClientFactory httpClientFactory, string server,
        bool useConsole) : base(logger, httpClientFactory, new ReCounterMessageHubClient(server, null), server, null,
        useConsole)
    {
    }

    public Task<Option<ErrorOmd[]>> IsCurrentUserValid(string token, CancellationToken cancellationToken = default)
    {
        return GetWithTokenAsync(token,
            CarcassApiRoutes.UserRights.UserRightsBase + CarcassApiRoutes.UserRights.IsCurrentUserValid,
            cancellationToken);
    }

    public Task<Result<LoginResponse>> Login(LoginRequest loginRequest, CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<LoginResponse>(
            CarcassApiRoutes.Authentication.AuthenticationBase + CarcassApiRoutes.Authentication.Login, false,
            JsonConvert.SerializeObject(loginRequest), cancellationToken);
    }

    public void SetToken(string accessToken)
    {
        AccessToken = accessToken;
        (MessageHubClient as ReCounterMessageHubClient)?.SetToken(AccessToken);
    }
}
