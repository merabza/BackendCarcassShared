namespace BackendCarcassShared.Contracts.V1.Requests;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ForgotPasswordRequest
{
    //მომხმარებლის სახელი, ან ელფოსტის მისამართი
    public string? UserNameOrEmail { get; set; }
}
