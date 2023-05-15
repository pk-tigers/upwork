using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using UpWork.Common.Exceptions;

namespace UpWork.Api.Filters
{
    public class UnauthorizedExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new UnauthorizedResult();
                context.ExceptionHandled = true;
            } else if (context.Exception is ForbiddenException)
            {
                context.Result = new ForbidResult();
                context.ExceptionHandled = true;
            }
        }
    }
}
