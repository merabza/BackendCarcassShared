using SystemTools.SharedKernel;

namespace BackendCarcassShared.Contracts.Errors;

public static class CarcassMasterDataErrors
{
    public static Error MustBeInteger(string fieldName, string? caption, string? defErrorCode, string? defErrorMessage)
    {
        return Error.Problem(defErrorCode ?? $"{fieldName}{nameof(MustBeInteger)}",
            defErrorMessage ?? $"{caption} მთელი უნდა იყოს");
    }

    public static Error MustBePositive(string fieldName, string? caption, string? defErrorCode, string? defErrorMessage)
    {
        return Error.Problem(defErrorCode ?? $"{fieldName}{nameof(MustBePositive)}",
            defErrorMessage ?? $"{caption} უნდა იყოს დადებითი რიცხვი");
    }

    public static Error Required(string fieldName, string? caption, string? defErrorCode, string? defErrorMessage)
    {
        return Error.Problem(defErrorCode ?? $"{fieldName}{nameof(Required)}",
            defErrorMessage ?? $"{caption} შევსებული უნდა იყოს");
    }

    public static Error MustBeBoolean(string fieldName, string? caption, string typeName)
    {
        return Error.Problem($"{fieldName}{nameof(MustBeBoolean)}", $"{caption} ველი უნდა იყოს {typeName} ტიპის");
    }

    public static Error IsEmpty(string fieldName, string? caption)
    {
        return Error.Problem($"{fieldName}{nameof(IsEmpty)}", $"{caption} შევსებული არ არის");
    }

    public static Error IsTooLong(string fieldName, string? caption)
    {
        return Error.Problem($"{fieldName}{nameof(IsTooLong)}", $"{caption} ძალიან გრძელია");
    }
}
