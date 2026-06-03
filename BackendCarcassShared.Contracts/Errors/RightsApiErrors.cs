using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

// ReSharper disable once ClassNeverInstantiated.Global
public static class RightsApiErrors
{
    public static readonly Error NoSufficientRights = new()
    {
        Code = nameof(NoSufficientRights), Name = "თქვენ არ გაქვთ საკმარისი უფლებები"
    };

    public static readonly Error ErrorWhenDeterminingCrudType = new()
    {
        Code = nameof(ErrorWhenDeterminingCrudType), Name = "შეცდომა ბაზაში ცვლილების მეთოდის დადგენისას"
    };

    public static readonly Error ErrorWhenDeterminingRights = new()
    {
        Code = nameof(ErrorWhenDeterminingRights), Name = "შეცდომა უფლებების დადგენისას"
    };

    public static readonly Error UserNotIdentified = new()
    {
        Code = nameof(UserNotIdentified), Name = "მომხმარებლის იდენტიფიცირება ვერ მოხერხდა"
    };

    public static readonly Error TableNameNotIdentified = new()
    {
        Code = nameof(TableNameNotIdentified), Name = "ცხრილის სახელის იდენტიფიცირება ვერ მოხერხდა"
    };

    public static readonly Error TableNamesListNotIdentified = new()
    {
        Code = nameof(TableNamesListNotIdentified), Name = "ცხრილის სახელების სიის იდენტიფიცირება ვერ მოხერხდა"
    };

    public static readonly Error InsufficientRights =
        new() { Code = nameof(InsufficientRights), Name = "არასაკმარისი უფლებები" };
}
