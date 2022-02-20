using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Assignment.WebApi.Filters
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var _apiKey = _configuration.GetValue<string>(key: "ApiKey");

            if (!context.HttpContext.Request.Query.TryGetValue("key", out var providedApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            if (!_apiKey.Equals(providedApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
 