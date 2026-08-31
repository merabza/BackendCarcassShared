using SystemTools.ApiContracts.Errors;
using SystemTools.SharedKernel;

namespace BackendCarcassShared.Contracts.Errors;

public static class CarcassApiErrors
{
    public static Error InvalidUser => Error.Problem(nameof(InvalidUser), "მომხმარებელი არასწორია");
    //public static readonly ErrorOmd RequestIsEmpty = new()
    //{
    //    Code = nameof(RequestIsEmpty), Name = "ატვირთული ინფორმაცია არასწორია"
    //};

    public static Error RequestIsEmpty => Error.Failure(nameof(RequestIsEmpty), "ატვირთული ინფორმაცია არასწორია");

    //public static readonly ErrorOmd ParametersAreInvalid =
    //    new() { Code = nameof(ParametersAreInvalid), Name = "პარამეტრები არასწორია" };

    public static Error ParametersAreInvalid => Error.Problem(nameof(ParametersAreInvalid), "პარამეტრები არასწორია");

    public static string IsEmptyErrCode => "{PropertyName}IsEmpty";

    public static string IsLongerThenErrCode => "{PropertyName}IsLongerThen{MaxLength}";

    public static Error IsLongerThen(string propertyNameLocalized, int maxLength) =>
        Error.Problem(IsLongerThenErrCode, ApiErrors.IsLongerThenErrMessage(propertyNameLocalized, maxLength));
}
