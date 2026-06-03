using System;
using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class MasterDataCrudErrors
{
    public static Error GridModelIsNull(string tableName)
    {
        return new Error { Code = nameof(GridModelIsNull), Name = $"gridModel is null for Table {tableName}" };
    }

    public static Error GenericMethodWasNotCreated(string methodName)
    {
        return new Error
        {
            Code = nameof(GenericMethodWasNotCreated), Name = $"Generic Method {methodName} was Not Created"
        };
    }

    public static Error MethodResultIsNull(string methodName)
    {
        return new Error { Code = nameof(MethodResultIsNull), Name = $"Method {methodName} Result Is Null" };
    }

    public static Error MethodResultTaskIsNull(string methodName)
    {
        return new Error { Code = nameof(MethodResultTaskIsNull), Name = $"Method {methodName} Result Task Is Null" };
    }

    public static Error SortIdHelperWasNotCreatedForType(Type type)
    {
        return new Error
        {
            Code = nameof(SortIdHelperWasNotCreatedForType),
            Name = $"SortIdHelper was not created for type {type.Name}"
        };
    }
}

/*
            return new Error[]
       { new() { Code = "ISortIdHelperIsNull", Name = "ISortIdHelper Is Null" } };
 */
