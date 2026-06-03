using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class UserRightsErrors
{
    public static readonly Error UserNotIdentifierSaveFiled = new()
    {
        Code = nameof(UserNotIdentifierSaveFiled),
        Name = "ვერ მოხერხდა მომხმარებლის იდენტიფიკაცია. მომხმარებლის ინფორმაციის შენახვა ვერ მოხერხდა"
    };

    public static readonly Error UserAuthenticationFailedThePasswordHasNotBeenChanged = new()
    {
        Code = nameof(UserAuthenticationFailedThePasswordHasNotBeenChanged),
        Name = "ვერ მოხერხდა მომხმარებლის იდენტიფიკაცია. პაროლი არ შეიცვალა"
    };

    public static readonly Error FailedToSaveUserInformation = new()
    {
        Code = nameof(FailedToSaveUserInformation), Name = "მომხმარებლის ინფორმაციის შენახვა ვერ მოხერხდა"
    };

    public static readonly Error FailedToChangePassword = new()
    {
        Code = nameof(FailedToChangePassword), Name = "პაროლის შეცვლა ვერ მოხერხდა"
    };

    public static readonly Error BadRequestFailedToDeleteUser = new()
    {
        Code = nameof(BadRequestFailedToDeleteUser), Name = "არასწორი მოთხოვნა, მომხმარებლის წაშლა ვერ მოხერხდა"
    };

    public static readonly Error NoUserFound = new() { Code = nameof(NoUserFound), Name = "მომხმარებელი არ მოიძებნა" };

    public static readonly Error DeletionErrorUserCouldNotBeDeleted = new()
    {
        Code = nameof(DeletionErrorUserCouldNotBeDeleted),
        Name = "წაშლისას მოხდა შეცდომა, მომხმარებლის წაშლა ვერ მოხერხდა"
    };

    public static readonly Error CouldNotLoadMenu = new()
    {
        Code = nameof(CouldNotLoadMenu), Name = "მენიუს ჩატვირთვა ვერ მოხერხდა"
    };
}
