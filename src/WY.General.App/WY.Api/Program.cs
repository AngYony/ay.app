using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WY.EntityFramework;
using WY.EntityFramework.Repositories;
using WY.Application.Articles;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


var con = builder.Configuration.GetConnectionString("Default");
//todo: AddDbContextPool与AddDbContextFactory
builder.Services.AddDbContext<WYDbContext>(
    options =>
        options.UseSqlServer(
            con,
            x => x.MigrationsAssembly("WY.EntityFramework.Migrations.SqlServer")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
builder.Services.AddTransient(typeof(IArticleService), typeof(ArticleServices));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(); 
builder.Services.AddAuthorization();
//允许跨域
builder.Services.AddCors(c => c.AddPolicy("any", p => {
    //更好的写法是p.WithHeaders()，p.WithOrigins()，只允许指定的域名或Header请求跨域
    p.AllowAnyHeader().AllowAnyHeader().AllowAnyOrigin();
}));
builder.Services.AddControllers(opt =>
{
    //全局应用Authorize属性
    var poliy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(poliy));
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
//允许所有的控制器跨域请求
app.UseCors("any");
app.MapControllers();

app.Run();
