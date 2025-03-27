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
            DbUpdateException { InnerException: SqlException { Number: 2627 } } => StatusCodes.Status409Conflict, // Unique constraint violation (SQL Server error code 2627)
            DbUpdateException { InnerException: SqlException { Number: 547 } } => StatusCodes.Status400BadRequest, // Foreign key constraint violation (SQL Server error code 547)
            DbUpdateException => StatusCodes.Status500InternalServerError, // Generic database error
            _ => StatusCodes.Status500InternalServerError
        };

        context.Result = new ObjectResult(new ProblemDetails
        {
            Title = "Ocorreu um erro",
            Detail = context.Exception.Message,
            Type = context.Exception.GetType().Name,
            Status = statusCode
        })
        {
            StatusCode = statusCode
        };
    }
}