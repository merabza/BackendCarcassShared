namespace BackendCarcassShared.Contracts.V1.Routes;

//SSO რეჟიმის (BFF) მისამართები აპის ჰოსტზე
public static class BffRoutes
{
    public const string BffBase = "/bff";

    // GET /bff/login?returnUrl=
    public const string Login = "/login";

    // POST /bff/logout (ფორმა antiforgery ტოკენით)
    public const string Logout = "/logout";

    // GET /bff/user
    public const string User = "/user";

    // POST /bff/backchannel (ფორმის ველი logout_token)
    public const string Backchannel = "/backchannel";

    //CSRF-ის header, რომელიც cookie-ით ავტორიზებულ ყოველ /api/* და /bff/* მოთხოვნას სჭირდება
    public const string CsrfHeaderName = "X-CSRF";
    public const string CsrfHeaderValue = "1";
}
