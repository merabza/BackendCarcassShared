using SystemTools.ApiContracts.Errors;
using SystemTools.SharedKernel;

namespace BackendCarcassShared.Contracts.Errors;

public static class AuthenticationApiErrors
{
    public static Error CouldNotCreateNewUser =>
        Error.Problem(nameof(CouldNotCreateNewUser), "ახალი მომხმარებლის შექმნა ვერ მოხერხდა");

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

    public static Error UserIsLockedOut =>
        Error.Problem(nameof(UserIsLockedOut),
            "წარუმატებელი ცდების გამო ანგარიში დროებით დაბლოკილია, სცადეთ 15 წუთის შემდეგ");

    public static Error InvalidOrExpiredCode =>
        Error.Problem(nameof(InvalidOrExpiredCode), "კოდი არასწორია, ან ვადაგასულია");

    public static Error TooManyCodeAttempts =>
        Error.Problem(nameof(TooManyCodeAttempts), "კოდის შეყვანის ცდების რაოდენობა ამოიწურა, მოითხოვეთ ახალი კოდი");

    public static Error CodeResendTooSoon =>
        Error.Problem(nameof(CodeResendTooSoon), "ახალი კოდის მოთხოვნა შესაძლებელია წუთში ერთხელ");

    public static Error InvalidPendingToken =>
        Error.Problem(nameof(InvalidPendingToken), "სესიის ვადა გავიდა, გაიარეთ ავტორიზაცია თავიდან");

    public static Error InvalidConfirmationLink =>
        Error.Problem(nameof(InvalidConfirmationLink), "ბმული არასწორია, ან ვადაგასულია");

    public static Error InvalidRefreshToken =>
        Error.Problem(nameof(InvalidRefreshToken), "სესიის განახლება ვერ მოხერხდა, გაიარეთ ავტორიზაცია თავიდან");

    public static Error EmailSendingFailed =>
        Error.Problem(nameof(EmailSendingFailed), "ელფოსტის გაგზავნა ვერ მოხერხდა, სცადეთ მოგვიანებით");

    public static string IsEmptyCodeErrMessage => ApiErrors.IsEmptyErrMessage("კოდი");
    public static string IsEmptyPendingTokenErrMessage => ApiErrors.IsEmptyErrMessage("დროებითი ტოკენი");
    public static string IsEmptyRefreshTokenErrMessage => ApiErrors.IsEmptyErrMessage("განახლების ტოკენი");
    public static string IsEmptyLinkTokenErrMessage => ApiErrors.IsEmptyErrMessage("ბმულის ტოკენი");

    public static string IsEmptyUserNameOrEmailErrMessage =>
        ApiErrors.IsEmptyErrMessage("მომხმარებლის სახელი, ან ელფოსტის მისამართი");

    public static string InvalidCodeFormatErrCode => "InvalidCodeFormat";
    public static string InvalidCodeFormatErrMessage => "კოდი 6 ციფრისგან უნდა შედგებოდეს";
    public static string InvalidUserIdErrCode => "InvalidUserId";
    public static string InvalidUserIdErrMessage => "მომხმარებლის იდენტიფიკატორი არასწორია";
    public static string ResetPasswordIdentificationErrCode => "ResetPasswordIdentification";

    public static string ResetPasswordIdentificationErrMessage =>
        "მიუთითეთ ან ბმულის მონაცემები, ან მომხმარებლის სახელი/ელფოსტა და კოდი";

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

    public static Error UserNameIsLongerThenErr(int maxLength) =>
        CarcassApiErrors.IsLongerThen("მომხმარებლის სახელის", maxLength);
}
