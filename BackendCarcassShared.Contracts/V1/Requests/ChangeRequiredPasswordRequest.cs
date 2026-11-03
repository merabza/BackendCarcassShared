namespace BackendCarcassShared.Contracts.V1.Requests;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ChangeRequiredPasswordRequest
{
    public string? PendingToken { get; set; }

    //ელფოსტაზე გაგზავნილი კოდი. საჭიროა მხოლოდ მაშინ, თუ ელფოსტა ამავე სესიაში ახლახან არ დადასტურებულა
    public string? Code { get; set; }

    public string? NewPassword { get; set; }
    public string? NewPasswordConfirm { get; set; }
}
