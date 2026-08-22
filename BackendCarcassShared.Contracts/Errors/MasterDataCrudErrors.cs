using System;
using SystemTools.SystemToolsShared.Errors;

namespace BackendCarcassShared.Contracts.Errors;

public static class MasterDataCrudErrors
{
    public static ErrorOmd GridModelIsNull(string tableName)
    {
        return new ErrorOmd { Code = nameof(GridModelIsNull), Name = $"gridModel is null for Table {tableName}" };
    }

    public static ErrorOmd GenericMethodWasNotCreated(string methodName)
    {
        return new ErrorOmd
        {
            Code = nameof(GenericMethodWasNotCreated), Name = $"Generic Method {methodName} was Not Created"
        };
    }

    public static ErrorOmd MethodResultIsNull(string methodName)
    {
        return new ErrorOmd { Code = nameof(MethodResultIsNull), Name = $"Method {methodName} Result Is Null" };
    }

    public static ErrorOmd MethodResultTaskIsNull(string methodName)
    {
        return new ErrorOmd
        {
            Code = nameof(MethodResultTaskIsNull), Name = $"Method {methodName} Result Task Is Null"
        };
    }

    public static ErrorOmd SortIdHelperWasNotCreatedForType(Type type)
    {
        return new ErrorOmd
        {
            Code = nameof(SortIdHelperWasNotCreatedForType),
            Name = $"SortIdHelper was not created for type {type.Name}"
        };
    }
}

/*
            return new ErrorOmd[]
       { new() { Code = "ISortIdHelperIsNull", Name = "ISortIdHelper Is Null" } };
 */
