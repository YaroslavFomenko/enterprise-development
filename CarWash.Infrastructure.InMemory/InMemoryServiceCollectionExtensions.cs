using CarWash.Application.Contracts.Repositories;
using CarWash.Infrastructure.InMemory.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CarWash.Infrastructure.InMemory;

/// <summary>
/// Регистрация репозиториев в памяти.
/// </summary>
public static class InMemoryServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует репозитории в памяти.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    public static IServiceCollection AddInMemoryRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IClientRepository, ClientRepository>();
        services.AddSingleton<ICarRepository, CarRepository>();
        services.AddSingleton<IServiceRepository, ServiceRepository>();
        services.AddSingleton<IOrderRepository, OrderRepository>();
        return services;
    }
}