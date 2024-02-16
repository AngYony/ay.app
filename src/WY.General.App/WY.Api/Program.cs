using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WY.EntityFramework;
using WY.EntityFramework.Repositories;
using WY.Application.Articles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var con = builder.Configuration.GetConnectionString("Default");
//builder.Services.AddDbContext<WYDbContext>(opt =>
//{
//    opt.UseSqlServer(con);
//});

//todo: AddDbContextPool”ÎAddDbContextFactory
builder.Services.AddDbContext<WYDbContext>(
    options =>
        options.UseSqlServer(
            con,
            x => x.MigrationsAssembly("WY.EntityFramework.Migrations.SqlServer")));




builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

builder.Services.AddTransient(typeof(IArticleService), typeof(ArticleServices));


builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
