using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class CarcassMasterDataErrors
{
    public static ErrorOmd MustBeInteger(string fieldName, string? caption, string? defErrorCode,
        string? defErrorMessage)
    {
        return new ErrorOmd
        {
            Code = defErrorCode ?? $"{fieldName}{nameof(MustBeInteger)}",
            Name = defErrorMessage ?? $"{caption} მთელი უნდა იყოს"
        };
    }

    public static ErrorOmd MustBePositive(string fieldName, string? caption, string? defErrorCode,
        string? defErrorMessage)
    {
        return new ErrorOmd
        {
            Code = defErrorCode ?? $"{fieldName}{nameof(MustBePositive)}",
            Name = defErrorMessage ?? $"{caption} უნდა იყოს დადებითი რიცხვი"
        };
    }

    public static ErrorOmd Required(string fieldName, string? caption, string? defErrorCode, string? defErrorMessage)
    {
        return new ErrorOmd
        {
            Code = defErrorCode ?? $"{fieldName}{nameof(Required)}",
            Name = defErrorMessage ?? $"{caption} შევსებული უნდა იყოს"
        };
    }

    public static ErrorOmd MustBeBoolean(string fieldName, string? caption, string typeName)
    {
        return new ErrorOmd
        {
            Code = $"{fieldName}{nameof(MustBeBoolean)}", Name = $"{caption} ველი უნდა იყოს {typeName} ტიპის"
        };
    }

    public static ErrorOmd IsEmpty(string fieldName, string? caption)
    {
        return new ErrorOmd { Code = $"{fieldName}{nameof(IsEmpty)}", Name = $"{caption} შევსებული არ არის" };
    }

    public static ErrorOmd IsTooLong(string fieldName, string? caption)
    {
        return new ErrorOmd { Code = $"{fieldName}{nameof(IsTooLong)}", Name = $"{caption} ძალიან გრძელია" };
    }
}
