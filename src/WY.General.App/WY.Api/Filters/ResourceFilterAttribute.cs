using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using WY.Application.Articles;

namespace WY.Api.Filters
{
    /// <summary>
    /// 资源过滤器，紧随授权筛选器执行，并拥有短路功能，适用于缓存，如果想要短路之后，仍然执行其他内容，可以使用AlwaysRunResultFilter过滤器
    /// </summary> 
    public class ResourceFilterAttribute : Attribute, IResourceFilter
    {
        private readonly IMemoryCache cache;
        private readonly IArticleService article;
        //引入了外部服务，需要使用TypeFilter引入该过滤器才可以使用IArticleService
        public ResourceFilterAttribute(IMemoryCache cache, IArticleService article)
        {
            this.cache = cache;
            this.article = article;
        }

        /// <summary>
        /// 只有在Context.Result没有赋值的情况下才会执行
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            var path = context.HttpContext.Request.Path.ToString();
            if (context.Result != null)
            {

                var value = (context.Result as ContentResult).Content;
                cache.Set(path, value, TimeSpan.FromHours(1));
            }
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var path = context.HttpContext.Request.Path.ToString();
            var hasValue = cache.TryGetValue(path, out object value);
            if (hasValue)
            {
                context.Result = new ContentResult
                {
                    Content = value.ToString()
                };
            }
            else
            {
                //一旦为Result设置了值，将直接短路返回结果，后续的过滤器均不会被执行。
                //context.Result = new ContentResult
                //{
                //    Content = article.GetTitle("使用TypeFiler引入过滤器")
                //};
            }
        }
    }
}
