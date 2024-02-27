using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WY.MVC.Models;

namespace WY.MVC.Controllers.Account
{
    public class LoginController : BaseController
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task LoginAsync(LoginViewModel model)
        {
            if (HttpContext.User.Identity.IsAuthenticated ||
                string.IsNullOrEmpty(model.UserName) || model.Password != "123123")
            {
                return;
            }

            // 身份声明列表
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.UserName),
            new Claim("FullName", model.UserName),
            new Claim(ClaimTypes.Role, "Administrator"),
            //如果不指定过期时间，默认是会话级
            new Claim(ClaimTypes.Expired, "1000")
        };

            // 使用指定的声明列表和认证方案实例化身份
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // 身份认证的扩展属性
            var authProperties = new AuthenticationProperties
            {
                // 是否允许刷新身份认证会话
                //AllowRefresh = <bool>,

                // 身份认证票据的绝对过期时间
                // 在Cookies中指Cookie有效期
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),

                // 是否长期有效
                // 在Cookies中指是否应该持久化Cookie
                IsPersistent = model.RememberMe,

                // 何时颁发的身份认证票据
                IssuedUtc = DateTimeOffset.UtcNow,

                // 登陆后的跳转Url
                RedirectUri = "Home/Index",
            };

            // 使用指定的身份实例化身份主体
            var identityPrincipal = new ClaimsPrincipal(claimsIdentity);

            // 使用指定的方案、身份主体和扩展属性登录用户，设置了RedirectUri时会自动跳转到指定地址
            // 如果不指定登录方案则使用默认登录方案，如果也没有设置默认登录方案则回退到默认方案，还没有设置默认方案则引发异常
            // 此处指定的方案必须在Startup中注册
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                identityPrincipal,
                authProperties);
        }


        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public IActionResult Logout(string redirectUri = "/")
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                // 注销指定方案的账户
                // 如果不指定方案则使用默认注销方案，没有配置则回退到默认方案，也没有配置默认方案则引发异常
                // 此处指定的方案必须在Startup中注册
                //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                //return new SignOutResult(CookieAuthenticationDefaults.AuthenticationScheme);
                return Redirect(redirectUri);
            }
            else
            {
                return View();
            }
        }
    }
}
