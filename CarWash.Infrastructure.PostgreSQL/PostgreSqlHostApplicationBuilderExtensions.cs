using CarWash.Application.Contracts.Repositories;
using CarWash.Infrastructure.PostgreSQL.Data;
using CarWash.Infrastructure.PostgreSQL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarWash.Infrastructure.PostgreSQL;

/// <summary>
/// Подключение репозиториев к PostgreSQL.
/// </summary>
public static class PostgreSqlHostApplicationBuilderExtensions
{
    /// <summary>
    /// Регистрирует контекст и репозитории.
    /// </summary>
    /// <param name="builder">Построитель приложения.</param>
    /// <param name="connectionName">Имя строки подключения.</param>
    /// <returns>Построитель приложения.</returns>
    public static IHostApplicationBuilder AddPostgreSqlRepositories(
        this IHostApplicationBuilder builder,
        string connectionName)
    {
        builder.AddNpgsqlDbContext<CarWashDbContext>(connectionName);
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<ICarRepository, CarRepository>();
        builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        return builder;
    }
}