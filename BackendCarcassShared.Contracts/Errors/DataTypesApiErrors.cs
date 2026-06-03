using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class DataTypesApiErrors
{
    public static readonly Error NoGridNamesInUriQuery = new()
    {
        Code = nameof(NoGridNamesInUriQuery), Name = "no grid names in uri query"
    };

    public static Error GridNotFound(string tableName)
    {
        return new Error { Code = nameof(GridNotFound), Name = $"Grid with key {tableName} not found" };
    }
}
