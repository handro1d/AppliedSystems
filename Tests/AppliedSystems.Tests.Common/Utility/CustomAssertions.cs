using System;
using NUnit.Framework;

namespace AppliedSystems.Tests.Common
{
    public static class CustomAssertions
    {
        public static void AssertArgumentNullExceptionThrown(Action action, string parameterName = null)
        {
            var exception = Assert.Throws<ArgumentNullException>(action.Invoke);

            if (!string.IsNullOrEmpty(parameterName))
            {
                Assert.AreEqual(parameterName, exception.ParamName);
            }
        }
    }
}
