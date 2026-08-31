using System;
using SystemTools.SharedKernel;

namespace BackendCarcassShared.Contracts.Errors;

public static class MasterDataCrudErrors
{
    public static Error GridModelIsNull(string tableName)
    {
        return Error.Problem(nameof(GridModelIsNull), $"gridModel is null for Table {tableName}");
    }

    public static Error GenericMethodWasNotCreated(string methodName)
    {
        return Error.Problem(nameof(GenericMethodWasNotCreated), $"Generic Method {methodName} was Not Created");
    }

    public static Error MethodResultIsNull(string methodName)
    {
        return Error.Problem(nameof(MethodResultIsNull), $"Method {methodName} Result Is Null");
    }

    public static Error MethodResultTaskIsNull(string methodName)
    {
        return Error.Problem(nameof(MethodResultTaskIsNull), $"Method {methodName} Result Task Is Null");
    }

    public static Error SortIdHelperWasNotCreatedForType(Type type)
    {
        return Error.Problem(nameof(SortIdHelperWasNotCreatedForType),
            $"SortIdHelper was not created for type {type.Name}");
    }
}

/*
            return new ErrorOmd[]
       { new() { Code = "ISortIdHelperIsNull", Name = "ISortIdHelper Is Null" } };
 */
