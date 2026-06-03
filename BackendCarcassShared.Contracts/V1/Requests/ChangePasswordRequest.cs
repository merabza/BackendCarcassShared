namespace BackendCarcassShared.Contracts.V1.Requests;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ChangePasswordRequest
{
    public int Userid { get; set; }
    public string? UserName { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? NewPasswordConfirm { get; set; }
}
