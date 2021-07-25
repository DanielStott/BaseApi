namespace BaseApi.Configuration.Middleware
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public static class ExceptionMiddleware
    {
        public static void UseCustomErrors(this IApplicationBuilder app)
        {
            app.Use(ProcessResponse);
        }

        private static async Task ProcessResponse(HttpContext httpContext, Func<Task> next)
        {
            await WriteResponse(httpContext);
        }

        private static async Task WriteResponse(HttpContext httpContext)
        {
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = exceptionDetails?.Error;

            if (ex is null)
                return;

            httpContext.Response.ContentType = "application/problem+json";
            var title = "An error occured";
            var details = ex.ToString();

            var problem = new ValidationProblemDetails
            {
                Status = 500,
                Title = title,
                Detail = details,
            };

            problem.Extensions["traceId"] = Activity.Current?.Id ?? httpContext?.TraceIdentifier;

            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}