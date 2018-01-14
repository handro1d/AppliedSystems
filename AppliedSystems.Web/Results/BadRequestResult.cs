using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AppliedSystems.Web
{
    public class BadRequestResult : ContentResult
    {
        private readonly string _statusDescription;

        public HttpStatusCode StatusCode { get; }

        public BadRequestResult(ModelStateDictionary modelState, HttpStatusCode statusCode = HttpStatusCode.BadRequest, string statusDescription = "Bad Request")
        {
            var errorMessages = (from modelStateValue in modelState.Values
                                 from modelStateError in modelStateValue.Errors
                                 select modelStateError.ErrorMessage).ToList();

            var errorString = string.Join(", ", errorMessages);

            Content = errorString;
            StatusCode = statusCode;
            _statusDescription = statusDescription;
        }

        public BadRequestResult(string content, HttpStatusCode statusCode = HttpStatusCode.BadRequest, string statusDescription = "Bad Request")
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException("content");
            }

            Content = content;
            StatusCode = statusCode;
            _statusDescription = statusDescription;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.StatusCode = (int) StatusCode;

            if (_statusDescription != null)
            {
                response.StatusDescription = _statusDescription;    
            }

            if (Content != null)
            {
                context.HttpContext.Response.Write(Content);
            }
        }
    }
}