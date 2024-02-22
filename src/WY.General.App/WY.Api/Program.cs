using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WY.EntityFramework;
using WY.EntityFramework.Repositories;
using WY.Application.Articles;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using WY.Application;
using WY.Api;

var builder = WebApplication.CreateBuilder(args);


var con = builder.Configuration.GetConnectionString("Default");
//todo: AddDbContextPool��AddDbContextFactory
builder.Services.AddDbContext<WYDbContext>(
    options =>
        options.UseSqlServer(
            con,
            x => x.MigrationsAssembly("WY.EntityFramework.Migrations.SqlServer")));



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAuthorization();
//�������
builder.Services.AddCors(c => c.AddPolicy("any", p =>
{
    //���õ�д����p.WithHeaders()��p.WithOrigins()��ֻ����ָ����������Header�������
    p.AllowAnyHeader().AllowAnyHeader().AllowAnyOrigin();
}));
builder.Services.AddControllers(opt =>
{
    ////ȫ��Ӧ��Authorize���Դ������Ա�ע
    //var poliy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    //opt.Filters.Add(new AuthorizeFilter(poliy));
})
.AddControllersAsServices();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<Autofac.ContainerBuilder>(builder =>
{
    builder.RegisterModule<ApplicationModule>();
    builder.RegisterModule<ApiModule>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
//�������еĿ�������������
app.UseCors("any");
app.MapControllers();

app.Run();
