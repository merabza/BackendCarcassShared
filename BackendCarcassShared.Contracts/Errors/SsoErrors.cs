using SystemTools.ApiContracts.Errors;
using SystemTools.SharedKernel;

namespace BackendCarcassShared.Contracts.Errors;

//sso.ltg.ge-ით შესვლის (SSO რეჟიმის) შეცდომები
public static class SsoErrors
{
    public static Error TokenRequestFailed =>
        Error.Problem(nameof(TokenRequestFailed), "შესვლის სერვისიდან წვდომის ტოკენის მიღება ვერ მოხერხდა");

    public static Error EmailLinkedToAnotherAccount =>
        Error.Conflict(nameof(EmailLinkedToAnotherAccount), "ელფოსტა უკვე დაკავშირებულია სხვა ანგარიშთან");

    public static Error SubjectIsMissing =>
        Error.Problem(nameof(SubjectIsMissing), "შესვლის სერვისის პასუხში მომხმარებლის იდენტიფიკატორი არ არის");

    public static Error UserNameIsMissing =>
        Error.Problem(nameof(UserNameIsMissing), "შესვლის სერვისის პასუხში მომხმარებლის სახელი არ არის");

    public static Error UnknownMachineClient =>
        Error.Problem(nameof(UnknownMachineClient), "მანქანური კლიენტი აპში რეგისტრირებული არ არის");
}
