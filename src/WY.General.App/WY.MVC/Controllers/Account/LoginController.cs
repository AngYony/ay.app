using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace WY.MVC.Controllers.Account
{
    public class LoginController : BaseController
    {
        public async Task Login(string username, string password)
        {
            //设置基于Cookie的登录方式
            ClaimsIdentity claimsIdentity = new("Ctm");
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, username));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "1"));
            //如果不指定过期时间，默认是会话级
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Expired, "1000"));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}
