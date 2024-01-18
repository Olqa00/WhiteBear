namespace WhiteBear.WebApi.Exceptions;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using WhiteBear.Application.Common.Exceptions;

internal sealed class ExceptionsHandler : IExceptionHandler
{
    private const string DEFAULT_TITLE = "An error occurred";
    private const string DEFAULT_SEPARATOR = ", ";

    private readonly IProblemDetailsService problemDetailsService;

    public ExceptionsHandler(IProblemDetailsService problemDetailsService)
    {
        this.problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = (int)HttpStatusCode.InternalServerError;
        var details = string.Empty;

        if (exception is ValidationException validationException)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            details = validationException.Message;
        }

        httpContext.Response.StatusCode = statusCode;

        return await this.problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Title = DEFAULT_TITLE,
                Detail = details,
            },
            Exception = exception,
        });
    }
}

