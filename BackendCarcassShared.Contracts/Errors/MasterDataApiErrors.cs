using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class MasterDataApiErrors
{
    public static Error NoRightsForCreate =>
        new() { Code = nameof(NoRightsForCreate), Name = "თქვენ არ გაქვთ უფლება შექმნათ ჩანაწერი ამ ცხრილში" };

    public static Error NoRightsForUpdate =>
        new() { Code = nameof(NoRightsForUpdate), Name = "თქვენ არ გაქვთ უფლება შეცვალოთ ჩანაწერი ამ ცხრილში" };

    public static Error NoRightsForDelete =>
        new() { Code = nameof(NoRightsForDelete), Name = "თქვენ არ გაქვთ უფლება წაშალოთ ჩანაწერი ამ ცხრილში" };

    public static Error CannotCreateNewRecord =>
        new() { Code = nameof(CannotCreateNewRecord), Name = "ახალი ჩანაწერის შექმნა ვერ მოხერხდა" };

    public static Error CannotLoad =>
        new() { Code = nameof(CannotLoad), Name = "მონაცემთა ბაზიდან ინფორმაციის ჩატვირთვა ვერ მოხერხდა" };

    public static Error CannotFindUser => new() { Code = nameof(CannotFindUser), Name = "მომხმარებელი ვერ მოიძებნა" };

    public static Error CannotFindRole => new() { Code = nameof(CannotFindRole), Name = "როლი ვერ მოიძებნა" };

    public static Error CannotUpdateNewRecord =>
        new() { Code = nameof(CannotUpdateNewRecord), Name = "ჩანაწერის შეცვლა ვერ მოხერხდა" };

    public static Error CannotDeleteNewRecord =>
        new() { Code = nameof(CannotDeleteNewRecord), Name = "ჩანაწერის წაშლა ვერ მოხერხდა" };

    public static Error EntryNotFound()
    {
        return new Error { Code = nameof(EntryNotFound), Name = "ჩანაწერის პოვნა ვერ მოხერხდა" };
    }

    public static Error TableNotFound(string tableName)
    {
        var err = new Error
        {
            Code = $"{tableName}{nameof(TableNotFound)}", Name = $"ცხრილი სახელით {tableName} არ არსებობს"
        };
        return err;
    }

    public static Error TableHaveNotSingleKey(string tableName)
    {
        var err = new Error
        {
            Code = $"{tableName}{nameof(TableHaveNotSingleKey)}",
            Name = $"ცხრილს სახელით {tableName} არ აქვს ერთადერთი გასაღები"
        };
        return err;
    }

    public static Error TableSingleKeyMustHaveOneProperty(string tableName)
    {
        var err = new Error
        {
            Code = $"{tableName}{nameof(TableSingleKeyMustHaveOneProperty)}",
            Name = $"ცხრილს სახელით {tableName} ერთადერთ გასაღებში არ აქვს ზუსტად ერთი ველი"
        };
        return err;
    }

    public static Error SetMethodNotFoundForTable(string tableName)
    {
        return new Error
        {
            Code = $"{nameof(SetMethodNotFoundForTable)}{tableName}",
            Name = $"ცხრილს სახელით {tableName} არ აქვს მეთოდი Set"
        };
    }

    public static Error SetMethodReturnsNullForTable(string tableName)
    {
        return new Error
        {
            Code = $"{nameof(SetMethodReturnsNullForTable)}{tableName}",
            Name = $"{tableName} ცხრილის Set მეთოდი აბრუნებს null-ს"
        };
    }

    public static Error RecordDoesNotDeserialized(string tableName)
    {
        return new Error
        {
            Code = $"{tableName}{nameof(RecordDoesNotDeserialized)}",
            Name = $"მიღებული ჩანაწერის  გაშიფვრა ვერ მოხერხდა {tableName} ცხრილის სტრუქტურის მიხედვით"
        };
    }

    public static Error WrongId(string tableName)
    {
        return new Error
        {
            Code = $"{tableName}{nameof(WrongId)}",
            Name =
                $"{tableName} ცხრილისთვის მოწოდებული ინფორმაცია არასწორია, რადგან იდენტიფიკატორი არ ემთხვევა მოწოდებული ობიექტის იდენტიფიკატორს"
        };
    }

    public static Error LoaderForTableNotFound(string tableName)
    {
        return new Error
        {
            Code = "LoaderForTableNotFound", Name = $"ჩამტვირთავი ცხრილისთვის სახელით {tableName} ვერ მოიძებნა"
        };
    }

    public static Error RecordNotFound(string tableName, int id)
    {
        return new Error
        {
            Code = $"{nameof(RecordNotFound)}{tableName}{id}",
            Name = $"ბაზაში {tableName} ცხრილში {id} იდენტიფიკატორის შესაბამისი ჩანაწერი არ არის ნაპოვნი"
        };
    }

    ////ბაზაში ვერ ვიპოვეთ მოწოდებული იდენტიფიკატორის შესაბამისი ჩანაწერი.

    public static Error MasterDataTableNotFound(string tableName)
    {
        return new Error { Code = nameof(MasterDataTableNotFound), Name = $"მონაცემთა ტიპი {tableName} ვერ მოიძებნა" };
    }

    public static Error MasterDataInvalidValidationRules(string tableName)
    {
        return new Error
        {
            Code = nameof(MasterDataTableNotFound),
            Name = $"მონაცემთა ტიპი {tableName} შეიცავს ვალიდაციის არასწორ წესებს"
        };
    }

    public static Error MasterDataFieldNotFound(string tableName, string fieldName)
    {
        return new Error
        {
            Code = nameof(MasterDataFieldNotFound),
            Name = $"მონაცემთა ტიპის {tableName} ველი {fieldName} შემოწმებისას ვერ მოიძებნა"
        };
    }
}
