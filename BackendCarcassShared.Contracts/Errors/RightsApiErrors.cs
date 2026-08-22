using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

// ReSharper disable once ClassNeverInstantiated.Global
public static class RightsApiErrors
{
    public static readonly ErrorOmd NoSufficientRights = new()
    {
        Code = nameof(NoSufficientRights), Name = "თქვენ არ გაქვთ საკმარისი უფლებები"
    };

    public static readonly ErrorOmd ErrorWhenDeterminingCrudType = new()
    {
        Code = nameof(ErrorWhenDeterminingCrudType), Name = "შეცდომა ბაზაში ცვლილების მეთოდის დადგენისას"
    };

    public static readonly ErrorOmd ErrorWhenDeterminingRights = new()
    {
        Code = nameof(ErrorWhenDeterminingRights), Name = "შეცდომა უფლებების დადგენისას"
    };

    public static readonly ErrorOmd UserNotIdentified = new()
    {
        Code = nameof(UserNotIdentified), Name = "მომხმარებლის იდენტიფიცირება ვერ მოხერხდა"
    };

    public static readonly ErrorOmd TableNameNotIdentified = new()
    {
        Code = nameof(TableNameNotIdentified), Name = "ცხრილის სახელის იდენტიფიცირება ვერ მოხერხდა"
    };

    public static readonly ErrorOmd TableNamesListNotIdentified = new()
    {
        Code = nameof(TableNamesListNotIdentified), Name = "ცხრილის სახელების სიის იდენტიფიცირება ვერ მოხერხდა"
    };

    public static readonly ErrorOmd InsufficientRights =
        new() { Code = nameof(InsufficientRights), Name = "არასაკმარისი უფლებები" };
}
