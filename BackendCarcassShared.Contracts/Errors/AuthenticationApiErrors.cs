using SystemTools.ApiContracts.Errors;
using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class AuthenticationApiErrors
{
    public static readonly Error UserAlreadyExists = new()
    {
        Code = nameof(UserAlreadyExists), Name = "მომხმარებელი ასეთი სახელით უკვე არსებობს"
    };

    public static readonly Error UsernameOrPasswordIsIncorrect = new()
    {
        Code = nameof(UsernameOrPasswordIsIncorrect), Name = "მომხმარებლის სახელი, ან პაროლი არასწორია "
    };

    public static readonly Error EmailAlreadyExists = new()
    {
        Code = nameof(EmailAlreadyExists), Name = "მომხმარებელი ასეთი ელექტრონული მისამართით უკვე არსებობს"
    };

    public static readonly Error MoreComplexPasswordIsRequired = new()
    {
        Code = nameof(MoreComplexPasswordIsRequired),
        Name = "პაროლის გამოყენება ვერ მოხერხდა, საჭიროა უფრო რთული პაროლი"
    };

    public static readonly Error CouldNotCreateNewUser = new()
    {
        Code = nameof(CouldNotCreateNewUser), Name = "ახალი მომხმარებლის შექმნა ვერ მოხერხდა"
    };

    public static readonly Error InvalidUsername =
        new() { Code = nameof(InvalidUsername), Name = "არასწორი მომხმარებლის სახელი" };

    public static readonly Error InvalidEmail = new()
    {
        Code = nameof(InvalidEmail), Name = "არასწორი ელექტრონული ფოსტის მისამართი"
    };

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

    public static Error UserNameIsLongerThenErr(int maxLength)
    {
        return CarcassApiErrors.IsLongerThen("მომხმარებლის სახელის", maxLength);
    }
}
