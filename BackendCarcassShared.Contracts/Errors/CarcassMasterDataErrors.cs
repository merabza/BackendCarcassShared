using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class CarcassMasterDataErrors
{
    public static Error MustBeInteger(string fieldName, string? caption, string? defErrorCode, string? defErrorMessage)
    {
        return new Error
        {
            Code = defErrorCode ?? $"{fieldName}{nameof(MustBeInteger)}",
            Name = defErrorMessage ?? $"{caption} მთელი უნდა იყოს"
        };
    }

    public static Error MustBePositive(string fieldName, string? caption, string? defErrorCode, string? defErrorMessage)
    {
        return new Error
        {
            Code = defErrorCode ?? $"{fieldName}{nameof(MustBePositive)}",
            Name = defErrorMessage ?? $"{caption} უნდა იყოს დადებითი რიცხვი"
        };
    }

    public static Error Required(string fieldName, string? caption, string? defErrorCode, string? defErrorMessage)
    {
        return new Error
        {
            Code = defErrorCode ?? $"{fieldName}{nameof(Required)}",
            Name = defErrorMessage ?? $"{caption} შევსებული უნდა იყოს"
        };
    }

    public static Error MustBeBoolean(string fieldName, string? caption, string typeName)
    {
        return new Error
        {
            Code = $"{fieldName}{nameof(MustBeBoolean)}", Name = $"{caption} ველი უნდა იყოს {typeName} ტიპის"
        };
    }

    public static Error IsEmpty(string fieldName, string? caption)
    {
        return new Error { Code = $"{fieldName}{nameof(IsEmpty)}", Name = $"{caption} შევსებული არ არის" };
    }

    public static Error IsTooLong(string fieldName, string? caption)
    {
        return new Error { Code = $"{fieldName}{nameof(IsTooLong)}", Name = $"{caption} ძალიან გრძელია" };
    }
}
