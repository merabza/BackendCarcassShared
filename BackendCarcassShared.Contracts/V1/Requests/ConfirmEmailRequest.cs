namespace BackendCarcassShared.Contracts.V1.Requests;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ConfirmEmailRequest
{
    //ავტორიზაციის, ან რეგისტრაციის პასუხად მიღებული დროებითი ტოკენი
    public string? PendingToken { get; set; }

    //ელფოსტაზე გაგზავნილი 6-ნიშნა კოდი
    public string? Code { get; set; }
}
