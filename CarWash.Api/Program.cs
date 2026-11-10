using System.Reflection;
using System.Text.Json.Serialization;
using CarWash.Api.Infrastructure.Repositories;
using CarWash.Api.Services;
using CarWash.Application.Contracts.Interfaces;
using CarWash.Application.Contracts.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddSingleton<IClientRepository, ClientRepository>();
builder.Services.AddSingleton<ICarRepository, CarRepository>();
builder.Services.AddSingleton<IServiceRepository, ServiceRepository>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();

builder.Services.AddSingleton<IServiceService, ServiceService>();
builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<ICarService, CarService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();