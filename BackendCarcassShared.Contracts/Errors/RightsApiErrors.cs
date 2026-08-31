using SystemTools.SharedKernel;

namespace BackendCarcassShared.Contracts.Errors;

// ReSharper disable once ClassNeverInstantiated.Global
public static class RightsApiErrors
{
    public static Error NoSufficientRights =>
        Error.Problem(nameof(NoSufficientRights), "თქვენ არ გაქვთ საკმარისი უფლებები");

    public static Error ErrorWhenDeterminingCrudType =>
        Error.Problem(nameof(ErrorWhenDeterminingCrudType), "შეცდომა ბაზაში ცვლილების მეთოდის დადგენისას");

    public static Error TableNamesListNotIdentified =>
        Error.Problem(nameof(TableNamesListNotIdentified), "ცხრილის სახელების სიის იდენტიფიცირება ვერ მოხერხდა");

    public static Error ErrorWhenDeterminingRights =>
        Error.Problem(nameof(ErrorWhenDeterminingRights), "შეცდომა უფლებების დადგენისას");

    public static Error UserNotIdentified =>
        Error.Problem(nameof(UserNotIdentified), "მომხმარებლის იდენტიფიცირება ვერ მოხერხდა");

    public static Error TableNameNotIdentified =>
        Error.Problem(nameof(TableNameNotIdentified), "ცხრილის სახელის იდენტიფიცირება ვერ მოხერხდა");

    public static Error InsufficientRights => Error.Problem(nameof(InsufficientRights), "არასაკმარისი უფლებები");
}
