using System.Data.SqlClient;

namespace AppliedSystems.Interfaces
{
    public interface ISqlExceptionRetryPolicy
    {
        bool IsTransientException(SqlException exception);
    }
}
