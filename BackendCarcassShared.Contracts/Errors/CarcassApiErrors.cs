using SystemTools.ApiContracts.Errors;
using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class CarcassApiErrors
{
    public static readonly Error RequestIsEmpty = new()
    {
        Code = nameof(RequestIsEmpty), Name = "ატვირთული ინფორმაცია არასწორია"
    };

    public static readonly Error ParametersAreInvalid =
        new() { Code = nameof(ParametersAreInvalid), Name = "პარამეტრები არასწორია" };

    public static readonly Error InvalidUser = new() { Code = nameof(InvalidUser), Name = "მომხმარებელი არასწორია" };

    public static string IsEmptyErrCode => "{PropertyName}IsEmpty";

    public static string IsLongerThenErrCode => "{PropertyName}IsLongerThen{MaxLength}";

    public static Error IsLongerThen(string propertyNameLocalized, int maxLength)
    {
        return new Error
        {
            Code = IsLongerThenErrCode, Name = ApiErrors.IsLongerThenErrMessage(propertyNameLocalized, maxLength)
        };
    }
}
