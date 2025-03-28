using FluentValidation;
using KeycloakIntegration.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KeycloakAuthIntegration.WebApi.Filters;

/// <summary>
/// Used to handle every Exception thrown during application execution
/// </summary>
public class GlobalExceptionFilter: IExceptionFilter
{
    /// <summary>
    /// Called when an Exception is thrown
    /// </summary>
    /// <param name="context">Exception Context</param>
    public void OnException(ExceptionContext context)
    {
        var statusCode = context.Exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            ForbiddenException => StatusCodes.Status403Forbidden,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            DbUpdateException { InnerException: SqlException { Number: 2601 } } => StatusCodes.Status409Conflict, // Unique constraint violation
            DbUpdateException { InnerException: SqlException { Number: 547 } } => StatusCodes.Status400BadRequest, // Foreign key violation
            DbUpdateException => StatusCodes.Status500InternalServerError, // Generic DB error
            _ => StatusCodes.Status500InternalServerError
        };

        var message = context.Exception switch
        {
            BadRequestException or UnauthorizedException or ForbiddenException or NotFoundException or ValidationException => context.Exception.Message,
            DbUpdateException { InnerException: SqlException { Number: 2601 } } => context.Exception.InnerException.Message, // Unique constraint violation
            DbUpdateException { InnerException: SqlException { Number: 547 } } => context.Exception.InnerException.Message, // Foreign key violation
            DbUpdateException => context.Exception.InnerException?.Message ?? context.Exception.Message, // Generic DB error
            _ => context.Exception.Message
        };

        context.Result = new ObjectResult(new ProblemDetails
        {
            Title = "Ocorreu um erro",
            Detail = message,
            Type = context.Exception.GetType().Name,
            Status = statusCode
        })
        {
            StatusCode = statusCode
        };
    }
}