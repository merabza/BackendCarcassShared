using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class CommonErrors
{
    public static readonly Error IncorrectData = new() { Code = nameof(IncorrectData), Name = "არასწორი მონაცემები" };

    public static Error CannotFindMethod(string methodName)
    {
        return new Error { Code = nameof(CannotFindMethod), Name = $"cannot find method {methodName}" };
    }
}
