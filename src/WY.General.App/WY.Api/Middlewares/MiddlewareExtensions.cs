namespace WY.Api.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCusMiddleware(this IApplicationBuilder app)
        {
            //引入异常处理中间件
            return app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
             ////自定义中间件处理方案
             //.Use(async (context, next) =>
             //{
             //    await next();

             //});
        }
    }
}
