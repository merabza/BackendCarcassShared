using System.Collections.Generic;

namespace BackendCarcassShared.Contracts.V1.Responses;

//SSO რეჟიმში მიმდინარე მომხმარებელი (GET /bff/user). ცვლის LoginResponse-ს: ტოკენები ბრაუზერში აღარ მოდის
public sealed class BffUserResponse
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    //როლები მძიმით გამოყოფილი, როგორც LoginResponse.RoleName-ში
    public string? RoleName { get; set; }

    public List<string> AppClaims { get; set; } = [];

    //გასვლის ფორმის (POST /bff/logout) antiforgery ტოკენი; ფორმის ველის სახელია LogoutFormFieldName
    public string? LogoutToken { get; set; }

    public string? LogoutFormFieldName { get; set; }
}
