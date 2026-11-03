namespace BackendCarcassShared.Contracts.V1.Requests;

//პაროლის აღდგენა ორი გზით შეიძლება:
//  1. ელფოსტაზე მისული ბმულიდან: UserId + Token
//  2. ელფოსტაზე მისული კოდით: UserNameOrEmail + Code
// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ResetPasswordRequest
{
    public int? UserId { get; set; }
    public string? Token { get; set; }
    public string? UserNameOrEmail { get; set; }
    public string? Code { get; set; }
    public string? NewPassword { get; set; }
    public string? NewPasswordConfirm { get; set; }
}
