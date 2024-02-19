using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WY.Api.Controllers
{
    // 允许跨域的控制器配置
    /*
     * 跨域配置的两种方式
     * 方式一：在控制器使用特性[EnableCors]
     * 方式二：在Program中使用app.UseCors("any");
     */
    [EnableCors("any")]
    public class CorsDemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
