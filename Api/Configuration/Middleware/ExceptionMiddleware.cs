using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Shared.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Configuration.Middleware;

public static class ExceptionMiddleware
{
    public static void Handler(this IApplicationBuilder app)
    {
        app.Use(ProcessResponse);
    }

    private static async Task ProcessResponse(HttpContext httpContext, Func<Task> next)
    {
        httpContext.Response.ContentType = "application/problem+json";
        var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
        var ex = exceptionDetails?.Error;

        switch (ex)
        {
            case ICustomException exception:
                await WriteCustomResponse(exception, httpContext);
                break;
            case ValidationException validationException:
                await WriteValidationResponse(validationException, httpContext);
                break;
            default:
                await WriteDefaultResponse(ex, httpContext);
                break;
        }
    }

    private static async Task WriteCustomResponse(ICustomException exception, HttpContext httpContext)
    {
        var problem = CreateProblemDetails(exception);

        await WriteResponse(httpContext, problem);
    }

    private static async Task WriteValidationResponse(ValidationException exception, HttpContext httpContext)
    {
        var problem = CreateValidationProblemDetails(exception);

        await WriteResponse(httpContext, problem);
    }

    private static async Task WriteDefaultResponse(Exception exception, HttpContext httpContext)
    {
        var problem = new ValidationProblemDetails
        {
            Title = "An error occured.",
            Status = 500,
            Detail = exception.Message,
        };

        await WriteResponse(httpContext, problem);
    }

    private static ProblemDetails CreateProblemDetails(ICustomException exception)
    {
        var problem = new ProblemDetails
        {
            Title = exception.Title,
            Status = exception.StatusCode,
            Detail = exception.Details,
            Type = exception.GetType().Name,
        };

        return problem;
    }

    private static ValidationProblemDetails CreateValidationProblemDetails(ValidationException exception)
    {
        var problem = new ValidationProblemDetails
        {
            Title = "Validation Error has occured.",
            Status = StatusCodes.Status400BadRequest,
            Detail = exception.Message,
            Type = exception.GetType().Name,
        };

        foreach (var error in exception.Errors)
        {
            if (problem.Errors.ContainsKey(error.PropertyName))
            {
                problem.Errors[error.PropertyName] =
                    problem.Errors[error.PropertyName].Append(error.ErrorMessage).ToArray();

                continue;
            }

            problem.Errors[error.PropertyName] = new[] { error.ErrorMessage };
        }

        return problem;
    }

    private static async Task WriteResponse(HttpContext httpContext, ValidationProblemDetails problem)
    {
        httpContext.Response.ContentType = "application/problem+json";
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        problem.Extensions["traceId"] = Activity.Current?.Id ?? httpContext?.TraceIdentifier;

        await httpContext.Response.WriteAsJsonAsync(problem);
    }

    private static async Task WriteResponse(HttpContext httpContext, ProblemDetails problem)
    {
        httpContext.Response.ContentType = "application/problem+json";
        httpContext.Response.StatusCode = problem.Status ?? 500;
        problem.Extensions["traceId"] = Activity.Current?.Id ?? httpContext?.TraceIdentifier;

        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}