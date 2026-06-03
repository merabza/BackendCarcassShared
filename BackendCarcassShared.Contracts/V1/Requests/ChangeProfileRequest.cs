namespace BackendCarcassShared.Contracts.V1.Requests;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ChangeProfileRequest
{
    public int Userid { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
