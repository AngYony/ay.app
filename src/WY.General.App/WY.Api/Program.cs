using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using WY.Api;
using WY.Api.Filters;
using WY.Application;
using WY.EntityFramework;
using WY.EntityFramework.Repositories;

var builder = WebApplication.CreateBuilder(args);

//
var con = builder.Configuration.GetConnectionString("Default");
//todo: AddDbContextPool与AddDbContextFactory
builder.Services.AddDbContext<WYDbContext>(
    options =>
        options.UseSqlServer(
            con,
            x => x.MigrationsAssembly("WY.EntityFramework.Migrations.SqlServer")));

//允许跨域
builder.Services.AddCors(c => c.AddPolicy("any", p =>
{
    //更好的写法是p.WithHeaders()，p.WithOrigins()，只允许指定的域名或Header请求跨域
    p.AllowAnyHeader().AllowAnyHeader().AllowAnyOrigin();
}));

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddMemoryCache();

builder.Services.AddTransient(typeof(IRepository<,>), typeof(RepositoryBase<,>));

builder.Services.AddControllers(opt =>
{
    //全局应用Authorize属性代替特性标注
    var poliy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(poliy));

    opt.Filters.Add<ActionExceptionFilter>();
    opt.Filters.Add<GlobalActionFilter>();
}).AddControllersAsServices();

builder.AddAutofac();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCusSwagger();
}
app.UseMiddleware();
app.MapControllers();
app.Run();