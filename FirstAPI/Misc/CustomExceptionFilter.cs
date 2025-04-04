using FirstAPI.Models.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstAPI.Misc
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
           
            context.Result = new BadRequestObjectResult(new ErrorObject
            {
                ErrorMessage = context.Exception.Message,
                ErrorNumber = 500
            });
        }
    }
}
