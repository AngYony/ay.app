namespace WY.Api
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder app)
        {
            //引入异常处理中间件
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            app.UseStaticFiles();
            //允许所有的控制器跨域请求
            app.UseCors("any");
            app.UseAuthentication().UseRouting().UseAuthorization();

            return app;


             ////自定义中间件处理方案
             //.Use(async (context, next) =>
             //{
             //    await next();

             //});
        }
    }
}
