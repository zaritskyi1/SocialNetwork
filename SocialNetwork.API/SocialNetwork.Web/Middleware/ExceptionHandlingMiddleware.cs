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

            var problemDetails = CreateProblemDetails(ex);

            var result = new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status
            };

            return _executor.ExecuteAsync(actionContext, result);
        }

        private ProblemDetails CreateProblemDetails(Exception ex)
        {
            var problemDetails = new ProblemDetails()
            {
                Detail = ex.Message
            };

            switch (ex)
            {
                case IdentityException e:
                    problemDetails.Title = "Identity error occured.";
                    problemDetails.Detail = "Check errors for details.";
                    problemDetails.Extensions.Add("errors", e.Errors);
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    break;
                case EntityNotFoundException _:
                    problemDetails.Title = "Entity not found.";
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    break;
                case EntityExistsException _:
                    problemDetails.Title = "Entity already exists.";
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    break;
                case ModelValidationException _:
                    problemDetails.Title = "Model validation failed.";
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    break;
                case InvalidBusinessOperationException _:
                    problemDetails.Title = "Invalid Operation.";
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    break;
                case AccessDeniedException _:
                    problemDetails.Title = "Access denied.";
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    break;
                default:
                    problemDetails.Title = "Internal server error.";
                    problemDetails.Detail = "Unknown error occurred! Contact system administrator.";
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    break;
            }

            return problemDetails;
        }
    }
}
