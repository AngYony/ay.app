using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WY.Api.Filters;
using WY.Application.Articles;
using WY.Entities.BBS;

namespace WY.Api.Controllers
{
    

    public class ArticeController : BaseController
    {
        private readonly IArticleService articleService;

        public ArticeController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        /*
         * API在返回值时，常见的做法是直接返回具体值，
         * 更推荐的做法是使用ActionResult类进行包裹处理，
         * 因为可以在Swagger中看到状态码和具体值的结构，同时还满足系统方法如NotFound()，更多的适用范围。
         * 不推荐使用IActionResult，在Swagger中看不到返回的值结构。
         */

        [HttpGet("wy")]
        public async Task<ActionResult<List<Article>>> GetWy()
        {
            return await articleService.GetWy();

            //返回的ActionResult<>类型同时兼容：return NotFound();
        }

        [AcceptVerbs("Get", "Post", Route = "test")]
        [TypeFilter(typeof(ResourceFilterAttribute))]
        public IActionResult abcc()
        {
            return Content("同时支持Get和Post请求:"+DateTime.Now);
        }


        [HttpGet("error")]
        public IActionResult errortest()
        {
            throw new Exception("抛出异常，讲在中间件中被捕获");
        }

    }
}