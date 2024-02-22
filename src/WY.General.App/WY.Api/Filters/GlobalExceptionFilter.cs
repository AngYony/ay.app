using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WY.Api.Filters
{
    /// <summary>
    /// 全局过滤器
    /// </summary>
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = new ContentResult
            {
                Content = context.Exception.Message
            };
            return Task.CompletedTask;
        }
    }
}
