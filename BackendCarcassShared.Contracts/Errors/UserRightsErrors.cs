using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class UserRightsErrors
{
    public static readonly ErrorOmd UserNotIdentifierSaveFiled = new()
    {
        Code = nameof(UserNotIdentifierSaveFiled),
        Name = "ვერ მოხერხდა მომხმარებლის იდენტიფიკაცია. მომხმარებლის ინფორმაციის შენახვა ვერ მოხერხდა"
    };

    public static readonly ErrorOmd UserAuthenticationFailedThePasswordHasNotBeenChanged = new()
    {
        Code = nameof(UserAuthenticationFailedThePasswordHasNotBeenChanged),
        Name = "ვერ მოხერხდა მომხმარებლის იდენტიფიკაცია. პაროლი არ შეიცვალა"
    };

    public static readonly ErrorOmd FailedToSaveUserInformation = new()
    {
        Code = nameof(FailedToSaveUserInformation), Name = "მომხმარებლის ინფორმაციის შენახვა ვერ მოხერხდა"
    };

    public static readonly ErrorOmd FailedToChangePassword = new()
    {
        Code = nameof(FailedToChangePassword), Name = "პაროლის შეცვლა ვერ მოხერხდა"
    };

    public static readonly ErrorOmd BadRequestFailedToDeleteUser = new()
    {
        Code = nameof(BadRequestFailedToDeleteUser), Name = "არასწორი მოთხოვნა, მომხმარებლის წაშლა ვერ მოხერხდა"
    };

    public static readonly ErrorOmd NoUserFound =
        new() { Code = nameof(NoUserFound), Name = "მომხმარებელი არ მოიძებნა" };

    public static readonly ErrorOmd DeletionErrorUserCouldNotBeDeleted = new()
    {
        Code = nameof(DeletionErrorUserCouldNotBeDeleted),
        Name = "წაშლისას მოხდა შეცდომა, მომხმარებლის წაშლა ვერ მოხერხდა"
    };

    public static readonly ErrorOmd CouldNotLoadMenu = new()
    {
        Code = nameof(CouldNotLoadMenu), Name = "მენიუს ჩატვირთვა ვერ მოხერხდა"
    };
}
