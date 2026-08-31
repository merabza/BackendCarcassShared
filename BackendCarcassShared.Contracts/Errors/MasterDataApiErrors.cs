using SystemTools.SharedKernel;

namespace BackendCarcassShared.Contracts.Errors;

public static class MasterDataApiErrors
{
    public static Error NoRightsForCreate =>
        Error.Problem(nameof(NoRightsForCreate), "თქვენ არ გაქვთ უფლება შექმნათ ჩანაწერი ამ ცხრილში");

    public static Error NoRightsForUpdate =>
        Error.Problem(nameof(NoRightsForUpdate), "თქვენ არ გაქვთ უფლება შეცვალოთ ჩანაწერი ამ ცხრილში");

    public static Error NoRightsForDelete =>
        Error.Problem(nameof(NoRightsForDelete), "თქვენ არ გაქვთ უფლება წაშალოთ ჩანაწერი ამ ცხრილში");

    public static Error CannotCreateNewRecord =>
        Error.Problem(nameof(CannotCreateNewRecord), "ახალი ჩანაწერის შექმნა ვერ მოხერხდა");

    public static Error CannotLoad =>
        Error.Problem(nameof(CannotLoad), "მონაცემთა ბაზიდან ინფორმაციის ჩატვირთვა ვერ მოხერხდა");

    public static Error CannotFindUser => Error.Problem(nameof(CannotFindUser), "მომხმარებელი ვერ მოიძებნა");

    public static Error CannotFindRole => Error.Problem(nameof(CannotFindRole), "როლი ვერ მოიძებნა");

    public static Error CannotUpdateNewRecord =>
        Error.Problem(nameof(CannotUpdateNewRecord), "ჩანაწერის შეცვლა ვერ მოხერხდა");

    public static Error CannotDeleteNewRecord =>
        Error.Problem(nameof(CannotDeleteNewRecord), "ჩანაწერის წაშლა ვერ მოხერხდა");

    public static Error EntryNotFound()
    {
        return Error.Problem(nameof(EntryNotFound), "ჩანაწერის პოვნა ვერ მოხერხდა");
    }

    public static Error TableNotFound(string tableName)
    {
        return Error.Problem($"{tableName}{nameof(TableNotFound)}", $"ცხრილი სახელით {tableName} არ არსებობს");
    }

    public static Error TableHaveNotSingleKey(string tableName)
    {
        return Error.Problem($"{tableName}{nameof(TableHaveNotSingleKey)}",
            $"ცხრილს სახელით {tableName} არ აქვს ერთადერთი გასაღები");
    }

    public static Error TableSingleKeyMustHaveOneProperty(string tableName)
    {
        return Error.Problem($"{tableName}{nameof(TableSingleKeyMustHaveOneProperty)}",
            $"ცხრილს სახელით {tableName} ერთადერთ გასაღებში არ აქვს ზუსტად ერთი ველი");
    }

    public static Error SetMethodNotFoundForTable(string tableName)
    {
        return Error.Problem($"{nameof(SetMethodNotFoundForTable)}{tableName}",
            $"ცხრილს სახელით {tableName} არ აქვს მეთოდი Set");
    }

    public static Error SetMethodReturnsNullForTable(string tableName)
    {
        return Error.Problem($"{nameof(SetMethodReturnsNullForTable)}{tableName}",
            $"{tableName} ცხრილის Set მეთოდი აბრუნებს null-ს");
    }

    public static Error RecordDoesNotDeserialized(string tableName)
    {
        return Error.Problem($"{nameof(RecordDoesNotDeserialized)}{tableName}",
            $"მიღებული ჩანაწერის  გაშიფვრა ვერ მოხერხდა {tableName} ცხრილის სტრუქტურის მიხედვით");
    }

    public static Error WrongId(string tableName)
    {
        return Error.Problem($"{tableName}{nameof(WrongId)}",
            $"{tableName} ცხრილისთვის მოწოდებული ინფორმაცია არასწორია, რადგან იდენტიფიკატორი არ ემთხვევა მოწოდებული ობიექტის იდენტიფიკატორს");
    }

    public static Error LoaderForTableNotFound(string tableName)
    {
        return Error.Problem(nameof(LoaderForTableNotFound),
            $"ჩამტვირთავი ცხრილისთვის სახელით {tableName} ვერ მოიძებნა");
    }

    public static Error RecordNotFound(string tableName, int id)
    {
        return Error.Problem($"{nameof(RecordNotFound)}{tableName}{id}",
            $"ბაზაში {tableName} ცხრილში {id} იდენტიფიკატორის შესაბამისი ჩანაწერი არ არის ნაპოვნი");
    }

    ////ბაზაში ვერ ვიპოვეთ მოწოდებული იდენტიფიკატორის შესაბამისი ჩანაწერი.

    public static Error MasterDataTableNotFound(string tableName)
    {
        return Error.Problem(nameof(MasterDataTableNotFound), $"მონაცემთა ტიპი {tableName} ვერ მოიძებნა");
    }

    public static Error MasterDataInvalidValidationRules(string tableName)
    {
        return Error.Problem(nameof(MasterDataInvalidValidationRules),
            $"მონაცემთა ტიპი {tableName} შეიცავს ვალიდაციის არასწორ წესებს");
    }

    public static Error MasterDataFieldNotFound(string tableName, string fieldName)
    {
        return Error.Problem(nameof(MasterDataFieldNotFound),
            $"მონაცემთა ტიპის {tableName} ველი {fieldName} შემოწმებისას ვერ მოიძებნა");
    }
}
