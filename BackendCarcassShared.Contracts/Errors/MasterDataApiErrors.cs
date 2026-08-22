using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class MasterDataApiErrors
{
    public static ErrorOmd NoRightsForCreate =>
        new() { Code = nameof(NoRightsForCreate), Name = "თქვენ არ გაქვთ უფლება შექმნათ ჩანაწერი ამ ცხრილში" };

    public static ErrorOmd NoRightsForUpdate =>
        new() { Code = nameof(NoRightsForUpdate), Name = "თქვენ არ გაქვთ უფლება შეცვალოთ ჩანაწერი ამ ცხრილში" };

    public static ErrorOmd NoRightsForDelete =>
        new() { Code = nameof(NoRightsForDelete), Name = "თქვენ არ გაქვთ უფლება წაშალოთ ჩანაწერი ამ ცხრილში" };

    public static ErrorOmd CannotCreateNewRecord =>
        new() { Code = nameof(CannotCreateNewRecord), Name = "ახალი ჩანაწერის შექმნა ვერ მოხერხდა" };

    public static ErrorOmd CannotLoad =>
        new() { Code = nameof(CannotLoad), Name = "მონაცემთა ბაზიდან ინფორმაციის ჩატვირთვა ვერ მოხერხდა" };

    public static ErrorOmd CannotFindUser =>
        new() { Code = nameof(CannotFindUser), Name = "მომხმარებელი ვერ მოიძებნა" };

    public static ErrorOmd CannotFindRole => new() { Code = nameof(CannotFindRole), Name = "როლი ვერ მოიძებნა" };

    public static ErrorOmd CannotUpdateNewRecord =>
        new() { Code = nameof(CannotUpdateNewRecord), Name = "ჩანაწერის შეცვლა ვერ მოხერხდა" };

    public static ErrorOmd CannotDeleteNewRecord =>
        new() { Code = nameof(CannotDeleteNewRecord), Name = "ჩანაწერის წაშლა ვერ მოხერხდა" };

    public static ErrorOmd EntryNotFound()
    {
        return new ErrorOmd { Code = nameof(EntryNotFound), Name = "ჩანაწერის პოვნა ვერ მოხერხდა" };
    }

    public static ErrorOmd TableNotFound(string tableName)
    {
        var err = new ErrorOmd
        {
            Code = $"{tableName}{nameof(TableNotFound)}", Name = $"ცხრილი სახელით {tableName} არ არსებობს"
        };
        return err;
    }

    public static ErrorOmd TableHaveNotSingleKey(string tableName)
    {
        var err = new ErrorOmd
        {
            Code = $"{tableName}{nameof(TableHaveNotSingleKey)}",
            Name = $"ცხრილს სახელით {tableName} არ აქვს ერთადერთი გასაღები"
        };
        return err;
    }

    public static ErrorOmd TableSingleKeyMustHaveOneProperty(string tableName)
    {
        var err = new ErrorOmd
        {
            Code = $"{tableName}{nameof(TableSingleKeyMustHaveOneProperty)}",
            Name = $"ცხრილს სახელით {tableName} ერთადერთ გასაღებში არ აქვს ზუსტად ერთი ველი"
        };
        return err;
    }

    public static ErrorOmd SetMethodNotFoundForTable(string tableName)
    {
        return new ErrorOmd
        {
            Code = $"{nameof(SetMethodNotFoundForTable)}{tableName}",
            Name = $"ცხრილს სახელით {tableName} არ აქვს მეთოდი Set"
        };
    }

    public static ErrorOmd SetMethodReturnsNullForTable(string tableName)
    {
        return new ErrorOmd
        {
            Code = $"{nameof(SetMethodReturnsNullForTable)}{tableName}",
            Name = $"{tableName} ცხრილის Set მეთოდი აბრუნებს null-ს"
        };
    }

    public static ErrorOmd RecordDoesNotDeserialized(string tableName)
    {
        return new ErrorOmd
        {
            Code = $"{tableName}{nameof(RecordDoesNotDeserialized)}",
            Name = $"მიღებული ჩანაწერის  გაშიფვრა ვერ მოხერხდა {tableName} ცხრილის სტრუქტურის მიხედვით"
        };
    }

    public static ErrorOmd WrongId(string tableName)
    {
        return new ErrorOmd
        {
            Code = $"{tableName}{nameof(WrongId)}",
            Name =
                $"{tableName} ცხრილისთვის მოწოდებული ინფორმაცია არასწორია, რადგან იდენტიფიკატორი არ ემთხვევა მოწოდებული ობიექტის იდენტიფიკატორს"
        };
    }

    public static ErrorOmd LoaderForTableNotFound(string tableName)
    {
        return new ErrorOmd
        {
            Code = "LoaderForTableNotFound", Name = $"ჩამტვირთავი ცხრილისთვის სახელით {tableName} ვერ მოიძებნა"
        };
    }

    public static ErrorOmd RecordNotFound(string tableName, int id)
    {
        return new ErrorOmd
        {
            Code = $"{nameof(RecordNotFound)}{tableName}{id}",
            Name = $"ბაზაში {tableName} ცხრილში {id} იდენტიფიკატორის შესაბამისი ჩანაწერი არ არის ნაპოვნი"
        };
    }

    ////ბაზაში ვერ ვიპოვეთ მოწოდებული იდენტიფიკატორის შესაბამისი ჩანაწერი.

    public static ErrorOmd MasterDataTableNotFound(string tableName)
    {
        return new ErrorOmd
        {
            Code = nameof(MasterDataTableNotFound), Name = $"მონაცემთა ტიპი {tableName} ვერ მოიძებნა"
        };
    }

    public static ErrorOmd MasterDataInvalidValidationRules(string tableName)
    {
        return new ErrorOmd
        {
            Code = nameof(MasterDataTableNotFound),
            Name = $"მონაცემთა ტიპი {tableName} შეიცავს ვალიდაციის არასწორ წესებს"
        };
    }

    public static ErrorOmd MasterDataFieldNotFound(string tableName, string fieldName)
    {
        return new ErrorOmd
        {
            Code = nameof(MasterDataFieldNotFound),
            Name = $"მონაცემთა ტიპის {tableName} ველი {fieldName} შემოწმებისას ვერ მოიძებნა"
        };
    }
}
