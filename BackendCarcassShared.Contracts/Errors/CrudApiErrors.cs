using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class CrudApiErrors
{
    public static readonly ErrorOmd WeCouldNotFindARecordToEditInTheDatabase = new()
    {
        Code = nameof(WeCouldNotFindARecordToEditInTheDatabase),
        Name = "ბაზაში ვერ ვიპოვეთ დასარედაქტირებელი ჩანაწერი"
    };

    public static readonly ErrorOmd CouldNotCreateNewRecord = new()
    {
        Code = nameof(CouldNotCreateNewRecord), Name = "ახალი ჩანაწერის შექმნა ვერ მოხერხდა"
    };

    public static readonly ErrorOmd NoRecordToDeleteFound = new()
    {
        Code = nameof(NoRecordToDeleteFound), Name = "წასაშლელი ჩანაწერი ვერ მოიძებნა. წაშლა ვერ მოხერხდა"
    };

    public static readonly ErrorOmd VirtualMethodDoesNotImplemented = new()
    {
        Code = nameof(VirtualMethodDoesNotImplemented),
        Name = "იდენტიფიკატორის მიხედვით ინფორმაციის ჩატვირთვის მეთოდი არ არის იმპლემენტირებული"
    };

    public static readonly ErrorOmd UploadedInformationCouldNotBeDecrypted = new()
    {
        Code = nameof(UploadedInformationCouldNotBeDecrypted), Name = "ატვირთული ინფორმაციის გაშიფვრა ვერ მოხერხდა"
    };

    public static readonly ErrorOmd WrongIdentifier = new()
    {
        Code = nameof(WrongIdentifier), Name = "ატვირთული ინფორმაცია არასწორია. (არასწორი იდენტიფიკატორი.)"
    };
}
