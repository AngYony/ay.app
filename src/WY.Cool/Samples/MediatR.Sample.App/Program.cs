using MediatR;
using MediatR.Sample.App;
using WY.Domain.Abstractions.Events;
using WY.Domain.Buses.MediatR.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IEventBus), typeof(MediatREventBus));
builder.Services.AddScoped(typeof(IEventBus<>), typeof(MediatREventBus<>));
builder.Services.AddScoped(typeof(IEventStore), typeof(InProcessEventStore));
builder.Services.AddMediatR(typeof(WySampleHandler).Assembly);


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
