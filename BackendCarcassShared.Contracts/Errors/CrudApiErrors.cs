using SystemTools.SharedKernel;

namespace BackendCarcassShared.Contracts.Errors;

public static class CrudApiErrors
{
    public static Error CouldNotCreateNewRecord =>
        Error.Problem(nameof(CouldNotCreateNewRecord), "ახალი ჩანაწერის შექმნა ვერ მოხერხდა");

    public static Error NoRecordToDeleteFound =>
        Error.Problem(nameof(NoRecordToDeleteFound), "წასაშლელი ჩანაწერი ვერ მოიძებნა. წაშლა ვერ მოხერხდა");

    public static Error VirtualMethodDoesNotImplemented =>
        Error.Problem(nameof(VirtualMethodDoesNotImplemented),
            "იდენტიფიკატორის მიხედვით ინფორმაციის ჩატვირთვის მეთოდი არ არის იმპლემენტირებული");

    public static Error UploadedInformationCouldNotBeDecrypted =>
        Error.Problem(nameof(UploadedInformationCouldNotBeDecrypted), "ატვირთული ინფორმაციის გაშიფვრა ვერ მოხერხდა");

    public static Error WrongIdentifier =>
        Error.Problem(nameof(WrongIdentifier), "ატვირთული ინფორმაცია არასწორია. (არასწორი იდენტიფიკატორი.)");

    public static Error WeCouldNotFindARecordToEditInTheDatabase =>
        Error.Problem(nameof(WeCouldNotFindARecordToEditInTheDatabase),
            "ბაზაში ვერ ვიპოვეთ დასარედაქტირებელი ჩანაწერი");
}
