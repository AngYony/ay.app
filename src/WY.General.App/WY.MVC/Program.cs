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
//todo: AddDbContextPool��AddDbContextFactory
builder.Services.AddDbContext<WYDbContext>(
    options =>
        options.UseSqlServer(
            con,
            x => x.MigrationsAssembly("WY.EntityFramework.Migrations.SqlServer")));




builder.Services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));
builder.Services.AddTransient(typeof(IArticleService), typeof(ArticleServices));

//��֤
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
//��Ȩ
builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews(opt => {
    ////ȫ��Ӧ��Authorize���ԣ�����������Ϸ������Ա�ע
    var poliy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(poliy));

});

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    //�޸ľܾ����ʵ�·�ɵ�ַ
//    options.AccessDeniedPath = new PathString("/Admin/AccessDenied");
//    //�޸ĵ�¼��ַ��·��
//    //   options.LoginPath = new PathString("/Admin/Login");  
//    //�޸�ע����ַ��·��
//    //   options.LogoutPath = new PathString("/Admin/LogOut");
//    //ͳһϵͳȫ�ֵ�Cookie����
//    options.Cookie.Name = "MockSchoolCookieName";
//    // ��¼�û�Cookie����Ч�� 
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
//    //�Ƿ��Cookie���û�������ʱ�䡣
//    options.SlidingExpiration = true;
//});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
//�����֤�м��
app.UseAuthentication();
app.UseRouting();

//��Ȩ�м����������UseRouting֮��
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
