using SystemTools.ApiContracts.Errors;
using SystemTools.SharedKernel;
using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class AuthenticationApiErrors
{
    public static readonly ErrorOmd CouldNotCreateNewUser = new()
    {
        Code = nameof(CouldNotCreateNewUser), Name = "ახალი მომხმარებლის შექმნა ვერ მოხერხდა"
    };

    public static Error UserAlreadyExists =>
        Error.Conflict(nameof(UserAlreadyExists), "მომხმარებელი ასეთი სახელით უკვე არსებობს");

    public static Error EmailAlreadyExists =>
        Error.Conflict(nameof(EmailAlreadyExists), "მომხმარებელი ასეთი ელექტრონული მისამართით უკვე არსებობს");

    public static Error MoreComplexPasswordIsRequired =>
        Error.Problem(nameof(MoreComplexPasswordIsRequired),
            "პაროლის გამოყენება ვერ მოხერხდა, საჭიროა უფრო რთული პაროლი");

    public static Error UsernameOrPasswordIsIncorrect =>
        Error.Problem(nameof(UsernameOrPasswordIsIncorrect), "მომხმარებლის სახელი, ან პაროლი არასწორია ");

    public static Error InvalidUsername => Error.Problem(nameof(InvalidUsername), "არასწორი მომხმარებლის სახელი");
    public static Error InvalidEmail => Error.Problem(nameof(InvalidEmail), "არასწორი ელექტრონული ფოსტის მისამართი");

    public static string IsEmptyEmailErrMessage => ApiErrors.IsEmptyErrMessage("ელექტრონული ფოსტის მისამართი");
    public static string IsEmptyFirstNameErrMessage => ApiErrors.IsEmptyErrMessage("სახელი");
    public static string IsEmptyLastNameErrMessage => ApiErrors.IsEmptyErrMessage("გვარი");
    public static string IsEmptyUserNameErrMessage => ApiErrors.IsEmptyErrMessage("მომხმარებლის სახელი");
    public static string IsEmptyPasswordErrMessage => ApiErrors.IsEmptyErrMessage("პაროლი");
    public static string IsEmptyOldPasswordErrMessage => ApiErrors.IsEmptyErrMessage("ძველი პაროლი");
    public static string IsEmptyNewPasswordErrMessage => ApiErrors.IsEmptyErrMessage("ახალი პაროლი");
    public static string InvalidEmailAddressErrCode => "InvalidEmailAddress";
    public static string InvalidEmailAddressErrMessage => "ელექტრონული ფოსტის მისამართი არასწორია";
    public static string NameIsLongerThenErrMessage => ApiErrors.IsEmptyErrMessage("სახელის");
    public static string LastNameIsLongerThenErrMessage => ApiErrors.IsEmptyErrMessage("გვარის");
    public static string PasswordsDoNotMatchErrCode => "PasswordsDoNotMatch";
    public static string PasswordsDoNotMatchErrMessage => "პაროლები ერთმანეთს არ ემთხვევა";

    public static ErrorOmd UserNameIsLongerThenErr(int maxLength)
    {
        return CarcassApiErrors.IsLongerThen("მომხმარებლის სახელის", maxLength);
    }
}
