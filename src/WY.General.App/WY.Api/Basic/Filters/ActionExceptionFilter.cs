using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WY.Api.Filters
{
    /// <summary>
    /// Action异常过滤器，只能处理Action之后的异常，如果需要全局处理异常，需要使用中间件
    /// </summary>
    public class ActionExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            //context.Result = new ContentResult
            //{
            //    Content = context.Exception.Message
            //};
            return Task.CompletedTask;
        }
    }
}
