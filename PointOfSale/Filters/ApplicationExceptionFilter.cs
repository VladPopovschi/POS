using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PointOfSale.Application.Exceptions;

namespace PointOfSale.Filters
{
    public class ApplicationExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var exceptionMessage = context.Exception.Message;

            if (context.Exception is ValidationException)
                context.Result = new BadRequestObjectResult(exceptionMessage);

            return Task.CompletedTask;
        }
    }
}