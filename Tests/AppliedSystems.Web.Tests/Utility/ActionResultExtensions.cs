using System;
using System.Web.Mvc;
using NUnit.Framework;

namespace AppliedSystems.Web.Tests
{
    internal static class ActionResultExtensions
    {
        public static void AssertIsRedirect(this ActionResult result, string controllerName, string actionName)
        {
            var redirectResult = result as RedirectToRouteResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual(controllerName, redirectResult.RouteValues["controller"]);
            Assert.AreEqual(actionName, redirectResult.RouteValues["action"]);
        }

        public static ViewResult AssertIsView(this ActionResult result, string viewName = null)
        {
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);

            if (!string.IsNullOrEmpty(viewName))
            {
                Assert.IsTrue(viewResult.ViewName.Equals(viewName, StringComparison.InvariantCultureIgnoreCase));
            }

            return viewResult;
        }
    }
}
