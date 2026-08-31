using SystemTools.SharedKernel;

namespace BackendCarcassShared.Contracts.Errors;

public static class CommonErrors
{
    //public static readonly ErrorOmd
    //    IncorrectData = new() { Code = nameof(IncorrectData), Name = "არასწორი მონაცემები" };

    public static Error IncorrectData => Error.Problem(nameof(IncorrectData), "არასწორი მონაცემები");

    public static Error CannotFindMethod(string methodName) =>
        Error.Problem(nameof(CannotFindMethod), $"cannot find method {methodName}");
}
