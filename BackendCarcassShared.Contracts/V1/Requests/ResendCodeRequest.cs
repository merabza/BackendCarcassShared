namespace BackendCarcassShared.Contracts.V1.Requests;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ResendCodeRequest
{
    public string? PendingToken { get; set; }
}
