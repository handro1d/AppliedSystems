using System;
using System.Net;
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

        public static void AssertIsStatus(this ActionResult result, HttpStatusCode statusCode, string content = null)
        {
            if (!string.IsNullOrEmpty(content))
            {
                result.AssertIsBadRequestResultStatus(statusCode, content);
                return;
            }

            var statusCodeResult = result as HttpStatusCodeResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual((int)statusCode, statusCodeResult.StatusCode);
        }

        public static void AssertIsBadRequestResultStatus(
            this ActionResult result, 
            HttpStatusCode statusCode,
            string content = null)
        {
            var contentResult = result as BadRequestResult;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(statusCode, contentResult.StatusCode);

            if (!string.IsNullOrEmpty(content))
            {
                Assert.AreEqual(content, contentResult.Content);
            }
        }
    }
}
