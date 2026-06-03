using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class CrudApiErrors
{
    public static readonly Error WeCouldNotFindARecordToEditInTheDatabase = new()
    {
        Code = nameof(WeCouldNotFindARecordToEditInTheDatabase),
        Name = "ბაზაში ვერ ვიპოვეთ დასარედაქტირებელი ჩანაწერი"
    };

    public static readonly Error CouldNotCreateNewRecord = new()
    {
        Code = nameof(CouldNotCreateNewRecord), Name = "ახალი ჩანაწერის შექმნა ვერ მოხერხდა"
    };

    public static readonly Error NoRecordToDeleteFound = new()
    {
        Code = nameof(NoRecordToDeleteFound), Name = "წასაშლელი ჩანაწერი ვერ მოიძებნა. წაშლა ვერ მოხერხდა"
    };

    public static readonly Error VirtualMethodDoesNotImplemented = new()
    {
        Code = nameof(VirtualMethodDoesNotImplemented),
        Name = "იდენტიფიკატორის მიხედვით ინფორმაციის ჩატვირთვის მეთოდი არ არის იმპლემენტირებული"
    };

    public static readonly Error UploadedInformationCouldNotBeDecrypted = new()
    {
        Code = nameof(UploadedInformationCouldNotBeDecrypted), Name = "ატვირთული ინფორმაციის გაშიფვრა ვერ მოხერხდა"
    };

    public static readonly Error WrongIdentifier = new()
    {
        Code = nameof(WrongIdentifier), Name = "ატვირთული ინფორმაცია არასწორია. (არასწორი იდენტიფიკატორი.)"
    };
}
