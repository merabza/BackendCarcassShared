namespace BackendCarcassShared.Contracts.V1.Requests;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class LoginRequest
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
