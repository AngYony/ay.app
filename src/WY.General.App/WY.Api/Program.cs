using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WY.EntityFramework;
using WY.EntityFramework.Repositories;
using WY.Services.Articles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var con = builder.Configuration.GetConnectionString("Default");
//builder.Services.AddDbContext<WYDbContext>(opt =>
//{
//    opt.UseSqlServer(con);
//});


builder.Services.AddDbContext<WYDbContext>(
    options =>
        options.UseSqlServer(
            con,
            x => x.MigrationsAssembly("WY.Migrations.SqlServer")));




builder.Services.AddTransient(typeof(IRepository<>), typeof(RepositoryBase<>));

builder.Services.AddTransient(typeof(IArticleService), typeof(ArticleServices));



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
