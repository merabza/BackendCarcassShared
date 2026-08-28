using SystemTools.SharedKernel;
using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class CommonErrors
{
    //public static readonly ErrorOmd
    //    IncorrectData = new() { Code = nameof(IncorrectData), Name = "არასწორი მონაცემები" };

    public static Error IncorrectData => Error.Problem(nameof(IncorrectData), "არასწორი მონაცემები");

    public static ErrorOmd CannotFindMethod(string methodName)
    {
        return new ErrorOmd { Code = nameof(CannotFindMethod), Name = $"cannot find method {methodName}" };
    }
}
