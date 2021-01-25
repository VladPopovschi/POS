using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PointOfSale.Application.Exceptions;

namespace PointOfSale.Filters
{
    public class ApplicationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionMessage = context.Exception.Message;

            if (context.Exception is ValidationException)
                context.Result = new BadRequestObjectResult(exceptionMessage);
        }
    }
}