using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Attributes;

internal class RedisCacheAttribute(int DurationInSec = 90)
    : ActionFilterAttribute
{
    // 
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        ICacheService service = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
        // create cache key
        string CacheKey = CreateCacheKey(context.HttpContext.Request);
        // search with the key
        var CacheValue = await service.GetAsync(CacheKey);
        // => value
        if (CacheValue != null)
        {
            // return from cache
            context.Result = new ContentResult
            {
                Content = CacheValue,
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK
            };
            return;
        }
        // value == null =>
        // invoke()
        var executedContent = await next.Invoke();
        // set cache
        if (executedContent.Result is OkObjectResult result)
        {
            await service.SetAsync(CacheKey, result.Value, TimeSpan.FromSeconds(DurationInSec));
        }

    }

    private static string CreateCacheKey(HttpRequest request)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(request.Path + '?');
        foreach (var item in request.Query.OrderBy(q => q.Key))
        {
            builder.Append($"{item.Key}={item.Value}&");
        }
        return builder.ToString().Trim('&');
    }
}
