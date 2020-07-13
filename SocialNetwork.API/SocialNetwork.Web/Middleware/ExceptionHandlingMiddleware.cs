using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using SocialNetwork.BLL.Exceptions;

namespace SocialNetwork.Web.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly IActionResultExecutor<ObjectResult> _executor;
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next, IActionResultExecutor<ObjectResult> executor)
        {
            _next = next;
            _executor = executor;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            var routeData = context.GetRouteData();
            var actionContext = new ActionContext(context, routeData, new ActionDescriptor());

            var problemDetails = CreateProblemDetails(context, ex);

            var result = new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status
            };

            return _executor.ExecuteAsync(actionContext, result);
        }

        private ProblemDetails CreateProblemDetails(HttpContext context, Exception ex)
        {
            var problemDetails = new ProblemDetails()
            {
                Instance = context.Request.Path,
                Detail = ex.Message
            };

            //problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

            switch (ex)
            {
                case IdentityException e:
                    problemDetails.Title = "One or more identity errors occured.";
                    problemDetails.Detail = "Please refer to the errors property for additional details.";
                    problemDetails.Extensions.Add("errors", e.Errors);
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    break;
                case EntityNotFoundException _:
                    problemDetails.Title = "Entity Not found.";
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    break;
                case EntityAlreadyExistsException _:
                    problemDetails.Title = "You do not have access to this action.";
                    problemDetails.Status = StatusCodes.Status303SeeOther;
                    break;
                case ModelValidationException e:
                    problemDetails.Title = "User id does not match current user id.";
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    break;
                case InvalidOperationException _:
                    problemDetails.Title = "This operation is invalid.";
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    break;
                default:
                    problemDetails.Title = "Internal server error.";
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    break;
            }

            return problemDetails;
        }
    }
}
