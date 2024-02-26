using Microsoft.EntityFrameworkCore;
using WY.Application.Articles;
using WY.EntityFramework.Repositories;
using WY.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Autofac.Core;

var builder = WebApplication.CreateBuilder(args);

var con = builder.Configuration.GetConnectionString("Default");
//todo: AddDbContextPool与AddDbContextFactory
builder.Services.AddDbContext<WYDbContext>(
    options =>
        options.UseSqlServer(
            con,
            x => x.MigrationsAssembly("WY.EntityFramework.Migrations.SqlServer")));




builder.Services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));
builder.Services.AddTransient(typeof(IArticleService), typeof(ArticleService));

// 注册身份认证服务
builder.Services.AddAuthentication(options =>
{
    // 默认方案，就是个字符串标识，在没有指定更具体的方案时提供默认回退
    // 方案必须在下面注册详细配置
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // 默认身份验证方案，在验证身份时会覆盖默认方案的设置
    // 除此之外还有默认登录、注销、质询、拒绝等方案，均会在相应情况下覆盖回退用的默认方案
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
// 使用指定的方案名注册Cookie方案
// 此方案在上面被配置为默认身份验证和回退方案
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    //修改拒绝访问的路由地址
    options.AccessDeniedPath = new PathString("/Admin/AccessDenied");

    // 该方案的登录地址
    // 匿名访问需要授权的地址时供自动跳转功能使用，框架内置的身份认证组件会自动使用，自定义授权组件时需自行编写代码实现同样的功能
    options.LoginPath = "/Account/Login";
    // 该方案的注销地址
    options.LogoutPath = "/Account/Logout";
    //统一系统全局的Cookie名称
    options.Cookie.Name = "MyCookieName";
    // 登录用户Cookie的有效期 
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    //是否对Cookie启用滑动过期时间。
    options.SlidingExpiration = true;
})
//授权
.Services.AddAuthorization();


builder.Services.AddControllersWithViews(opt =>
{
    ////全局应用Authorize属性，代替控制器上方的特性标注
    //var poliy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    //opt.Filters.Add(new AuthorizeFilter(poliy));

});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
//身份认证中间件
app.UseAuthentication();
app.UseRouting();
//授权中间件，必须在UseRouting之后
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
