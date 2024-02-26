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
//todo: AddDbContextPool��AddDbContextFactory
builder.Services.AddDbContext<WYDbContext>(
    options =>
        options.UseSqlServer(
            con,
            x => x.MigrationsAssembly("WY.EntityFramework.Migrations.SqlServer")));




builder.Services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));
builder.Services.AddTransient(typeof(IArticleService), typeof(ArticleService));

// ע�������֤����
builder.Services.AddAuthentication(options =>
{
    // Ĭ�Ϸ��������Ǹ��ַ�����ʶ����û��ָ��������ķ���ʱ�ṩĬ�ϻ���
    // ��������������ע����ϸ����
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // Ĭ�������֤����������֤���ʱ�Ḳ��Ĭ�Ϸ���������
    // ����֮�⻹��Ĭ�ϵ�¼��ע������ѯ���ܾ��ȷ�������������Ӧ����¸��ǻ����õ�Ĭ�Ϸ���
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
// ʹ��ָ���ķ�����ע��Cookie����
// �˷��������汻����ΪĬ�������֤�ͻ��˷���
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    //�޸ľܾ����ʵ�·�ɵ�ַ
    options.AccessDeniedPath = new PathString("/Admin/AccessDenied");

    // �÷����ĵ�¼��ַ
    // ����������Ҫ��Ȩ�ĵ�ַʱ���Զ���ת����ʹ�ã�������õ������֤������Զ�ʹ�ã��Զ�����Ȩ���ʱ�����б�д����ʵ��ͬ���Ĺ���
    options.LoginPath = "/Account/Login";
    // �÷�����ע����ַ
    options.LogoutPath = "/Account/Logout";
    //ͳһϵͳȫ�ֵ�Cookie����
    options.Cookie.Name = "MyCookieName";
    // ��¼�û�Cookie����Ч�� 
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    //�Ƿ��Cookie���û�������ʱ�䡣
    options.SlidingExpiration = true;
})
//��Ȩ
.Services.AddAuthorization();


builder.Services.AddControllersWithViews(opt =>
{
    ////ȫ��Ӧ��Authorize���ԣ�����������Ϸ������Ա�ע
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
//�����֤�м��
app.UseAuthentication();
app.UseRouting();
//��Ȩ�м����������UseRouting֮��
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
