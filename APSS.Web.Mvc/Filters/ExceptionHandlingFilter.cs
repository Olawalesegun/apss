using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace APSS.Web.Mvc.Filters;

public class ExceptionHandlingFilter<T> : IExceptionFilter
    where T : Exception
{
    public const string UNHANDLED_ERRORS = "UnhandledErrors";

    private readonly IModelMetadataProvider _modelMetadataProvider;

    public ExceptionHandlingFilter(IModelMetadataProvider modelMetadataProvider)
        => _modelMetadataProvider = modelMetadataProvider;

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not T)
        {
            context.ExceptionHandled = false;
            return;
        }

        var result = new ViewResult
        {
            ViewData = new(_modelMetadataProvider, context.ModelState)
        };

        // TODO: Log the error, and use appropriate display message type

        context.ModelState.AddModelError(UNHANDLED_ERRORS, context.Exception.Message);

        context.ExceptionHandled = true;
        context.Result = result;
    }
}