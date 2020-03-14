using JMovies.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace JMovies.Web.Filters
{
    public class ResponseActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult)
            {
                object resultObject = ((ObjectResult)context.Result).Value;
                if (resultObject is BaseResponse)
                {
                    IContextProvider contextProvider = context.HttpContext.RequestServices.GetRequiredService<IContextProvider>();
                    ((BaseResponse)resultObject).Context = contextProvider.GetContext();
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
