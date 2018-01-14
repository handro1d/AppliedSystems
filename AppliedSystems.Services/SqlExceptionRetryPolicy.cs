using System;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using AppliedSystems.Interfaces;

namespace AppliedSystems.Services
{
    [ExcludeFromCodeCoverage]
    public class SqlExceptionRetryPolicy : ISqlExceptionRetryPolicy
    {
        public bool IsTransientException(SqlException exception)
        {
            return !Enum.IsDefined(typeof(NonTransientSqlException), exception.Number);
        }
    }

    internal enum NonTransientSqlException
    {
        PARAMETER_NOT_SUPPLIED = 201,
        CANNOT_INSERT_NULL_INTO_NON_NULL = 515,
        FOREGIN_KEY_VIOLATION = 547,
        PRIMARY_KEY_VIOLATION = 2627,
        MEMORY_ALLOCATION_FAILED = 4846,
        ERROR_CONVERTING_NUMERIC_TO_DECIMAL = 8114,
        TOO_MANY_ARGUMENTS = 8144,
        ARGUMENT_IS_NOT_A_PARAMETER = 8145,
        ARGS_SUPPLIED_FOR_PROCEDURE_WITHOUT_PARAMETERS = 8146,
        STRING_OR_BINARY_TRUNCATED = 8152,
        INVALID_POINTER = 10006,
        WRONG_NUMBER_OF_PARAMETERS = 18751
    }
}
