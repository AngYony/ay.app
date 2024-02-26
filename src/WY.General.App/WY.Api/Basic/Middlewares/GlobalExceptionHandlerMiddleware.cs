using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Mime;

namespace WY.Api
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Call the next delegate/middleware in the pipeline.
                await _next(context);//必不可少
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //在中间件中为Response的ContentType设置值时，需要注意过滤器的干扰，有可能会出现异常，提示ContentType正在被使用中，不能修改
            context.Response.ContentType = MediaTypeNames.Application.Json;
            var errorResponse = new ErrorResponse
            {
                Success = false
            };
            //判断类型
            switch (exception)
            {
                case ApplicationException ex:

                    break;

                case NotImplementedException ex:
                    break;

                default:
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.Message = exception.Message;
                        break;
                    }
            }

            var res = JsonConvert.SerializeObject(errorResponse, new JsonSerializerSettings()
            {
                // 设置为驼峰命名
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            await context.Response.WriteAsync(res);
        }
    }

    public class ErrorResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
    }
}