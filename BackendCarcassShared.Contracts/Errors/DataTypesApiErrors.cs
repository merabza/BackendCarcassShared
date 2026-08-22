using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class DataTypesApiErrors
{
    public static readonly ErrorOmd NoGridNamesInUriQuery = new()
    {
        Code = nameof(NoGridNamesInUriQuery), Name = "no grid names in uri query"
    };

    public static ErrorOmd GridNotFound(string tableName)
    {
        return new ErrorOmd { Code = nameof(GridNotFound), Name = $"Grid with key {tableName} not found" };
    }
}
