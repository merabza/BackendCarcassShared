namespace BackendCarcassShared.Contracts.V1.Requests;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ConfirmEmailLinkRequest
{
    //ბმულიდან წამოღებული მომხმარებლის იდენტიფიკატორი
    public int UserId { get; set; }

    //ბმულიდან წამოღებული ტოკენი
    public string? Token { get; set; }

    //თუ ბმული იმავე ბრაუზერში გაიხსნა, სადაც ავტორიზაცია დაიწყო, აქ იგზავნება შენახული დროებითი ტოკენი,
    //რომ ავტორიზაცია ავტომატურად დასრულდეს
    public string? PendingToken { get; set; }
}
