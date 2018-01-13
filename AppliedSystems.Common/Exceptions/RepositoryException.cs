using System;
using System.Diagnostics.CodeAnalysis;

namespace AppliedSystems.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class RepositoryException : Exception
    {
        private readonly RepositoryExceptionType _exceptionType;

        public new string Message => string.Format("A {0} respository exception has occurred.", _exceptionType);

        public new Exception InnerException { get; }

        public RepositoryException(RepositoryExceptionType exceptionType, Exception exception)
        {
            _exceptionType = exceptionType;
            InnerException = exception;
        }
    }

    public enum RepositoryExceptionType
    {
        Delete,
        Update
    }
}
