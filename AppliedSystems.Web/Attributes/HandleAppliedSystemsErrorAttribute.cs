using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using AppliedSystems.Common.Exceptions;
using AppliedSystems.Repository;
using HttpException = System.Web.HttpException;
using WindowsEventLog = System.Diagnostics.EventLog;

namespace AppliedSystems.Web
{
    public class HandleAppliedSystemsErrorAttribute : HandleErrorAttribute
    {
        private string EventLogSource { get; }
        private const string EventLog = "Application";

        public HandleAppliedSystemsErrorAttribute(string eventLogSource)
        {
            EventLogSource = eventLogSource;
        }

        public override void OnException(ExceptionContext exceptionContext)
        {
            var context = DependencyResolver.Current.GetService<AppliedSystemsContext>();

            if (exceptionContext.ExceptionHandled)
            {
                return;
            }

            if (new HttpException(null, exceptionContext.Exception).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(exceptionContext.Exception))
            {
                return;
            }

            Type exceptionType = exceptionContext.Exception.GetType();
            if (exceptionType.IsGenericType &&
                exceptionType.GetGenericTypeDefinition() == typeof(EntityNotFoundException<>))
            {
                var statusDescription = string.Format("{0} could not be found.",
                    exceptionType.GetGenericArguments().FirstOrDefault()?.Name);

                SetExceptionStatus(exceptionContext, 404, statusDescription);
            }
            else
            {
                SetExceptionStatus(exceptionContext, 500);
            }

            var controllerName = (string)exceptionContext.RouteData.Values["controller"];
            var actionName = (string)exceptionContext.RouteData.Values["action"];
            var model = new HandleErrorInfo(exceptionContext.Exception, controllerName, actionName);

            exceptionContext.Result = new ViewResult
            {
                ViewName = "/Views/Error/Internal.cshtml",
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model)
            };

            try
            {
                ClearEditiedEntities(context);

                var exception = exceptionContext.Exception;

                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }
            }
            catch (Exception ex)
            {
                // Write to Event Log
                //WriteToWindowsEventLog(exceptionContext.Exception.Message);
            }
        }

        private static void ClearEditiedEntities(AppliedSystemsContext context)
        {
            var changedEntries = context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList();

            foreach (DbEntityEntry entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Detached:
                    case EntityState.Unchanged:
                    default:
                        break;
                }
            }
        }

        private void SetExceptionStatus(ExceptionContext exceptionContext, int statusCode, string statusDescription = null)
        {
            exceptionContext.ExceptionHandled = true;
            exceptionContext.HttpContext.Response.Clear();
            exceptionContext.HttpContext.Response.StatusCode = statusCode;

            if (!string.IsNullOrEmpty(statusDescription))
            {
                exceptionContext.HttpContext.Response.StatusDescription = statusDescription;
            }

            exceptionContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        public void WriteToWindowsEventLog(string message)
        {
            if (!WindowsEventLog.SourceExists(EventLogSource))
            {
                WindowsEventLog.CreateEventSource(EventLogSource, EventLog);
            }

            WindowsEventLog.WriteEntry(EventLogSource, message);
        }
    }
}