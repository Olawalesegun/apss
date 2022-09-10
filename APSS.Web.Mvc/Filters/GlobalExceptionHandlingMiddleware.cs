using System.Net;

using APSS.Domain.Repositories.Exceptions;
using APSS.Domain.Repositories.Extensions.Exceptions;
using APSS.Domain.Services.Exceptions;
using APSS.Domain.ValueTypes.Exceptions;

namespace APSS.Web.Mvc.Filters;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // TODO: log error

            context.Response.StatusCode = (int)MapStatusCodeFromException(ex);
        }
    }

    private static HttpStatusCode MapStatusCodeFromException(Exception ex)
    {
        return ex switch
        {
            // Bad request
            InvalidCoordinatesException or
            InvalidDateTimeRangeException or
            InvalidLogicalQuestionAnswerException or
            InvalidMultipleChoiceQuestionAnswerException or
            InvalidTextQuestionAnswerException or
            InvalidPaginationParametersException => HttpStatusCode.BadRequest,

            // Unauthroized
            InvalidAccessLevelException or
            InvalidPermissionsExceptions or
            InsufficientPermissionsException or
            DisabledAccountException or
            InvalidAccountIdOrPasswordException or
            InvalidSessionException or
            ExpiredSessionException or
            MaxSessionsCountExceeded => HttpStatusCode.Unauthorized,

            // Not found
            NotFoundException or
            SurveyExpiredException => HttpStatusCode.NotFound,

            // Conflict
            SystemAlreadySetupException => HttpStatusCode.Conflict,

            // Internal server error
            _ => HttpStatusCode.InternalServerError,
        };
    }
}