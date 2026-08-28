using SystemTools.ApiContracts.Errors;
using SystemTools.SharedKernel;
using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class CarcassApiErrors
{
    public static readonly ErrorOmd InvalidUser = new() { Code = nameof(InvalidUser), Name = "მომხმარებელი არასწორია" };
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

    public static ErrorOmd IsLongerThen(string propertyNameLocalized, int maxLength)
    {
        return new ErrorOmd
        {
            Code = IsLongerThenErrCode, Name = ApiErrors.IsLongerThenErrMessage(propertyNameLocalized, maxLength)
        };
    }
}
