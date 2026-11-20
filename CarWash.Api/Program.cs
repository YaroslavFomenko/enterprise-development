using CarWash.Application.Contracts.Interfaces;
using CarWash.Application.Services;
using CarWash.Infrastructure.InMemory;
using CarWash.Infrastructure.PostgreSQL;
using CarWash.Infrastructure.PostgreSQL.Data;
using CarWash.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var provider = builder.Configuration["Storage:Provider"] ?? "PostgreSQL";
switch (provider)
{
    case "InMemory":
        builder.Services.AddInMemoryRepositories();
        break;
    case "PostgreSQL":
        builder.AddPostgreSqlRepositories("carwash");
        break;
    default:
        throw new InvalidOperationException($"Неизвестное хранилище: {provider}.");
}

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (provider == "PostgreSQL")
{
    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<CarWashDbContext>();
    await db.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapDefaultEndpoints();
app.Run();