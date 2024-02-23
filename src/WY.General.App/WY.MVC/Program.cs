using Microsoft.EntityFrameworkCore;
using WY.Application.Articles;
using WY.EntityFramework.Repositories;
using WY.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

var con = builder.Configuration.GetConnectionString("Default");
//todo: AddDbContextPool与AddDbContextFactory
builder.Services.AddDbContext<WYDbContext>(
    options =>
        options.UseSqlServer(
            con,
            x => x.MigrationsAssembly("WY.EntityFramework.Migrations.SqlServer")));




builder.Services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));
builder.Services.AddTransient(typeof(IArticleService), typeof(ArticleServices));

//认证
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
//授权
builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews(opt => {
    ////全局应用Authorize属性，代替控制器上方的特性标注
    var poliy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(poliy));

});

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    //修改拒绝访问的路由地址
//    options.AccessDeniedPath = new PathString("/Admin/AccessDenied");
//    //修改登录地址的路由
//    //   options.LoginPath = new PathString("/Admin/Login");  
//    //修改注销地址的路由
//    //   options.LogoutPath = new PathString("/Admin/LogOut");
//    //统一系统全局的Cookie名称
//    options.Cookie.Name = "MockSchoolCookieName";
//    // 登录用户Cookie的有效期 
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
//    //是否对Cookie启用滑动过期时间。
//    options.SlidingExpiration = true;
//});



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
