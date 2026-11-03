namespace BackendCarcassShared.Contracts.V1.Requests;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class LogoutRequest
{
    public string? RefreshToken { get; set; }
}
