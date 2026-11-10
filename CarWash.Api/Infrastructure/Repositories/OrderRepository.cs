using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Data;
using CarWash.Domain.Entities;

namespace CarWash.Api.Infrastructure.Repositories;

/// <summary>
/// Репозиторий заказов в памяти.
/// </summary>
public class OrderRepository() : InMemoryRepository<Order>(CarWashDataSeeder.GetOrders(
    CarWashDataSeeder.GetClients(),
    CarWashDataSeeder.GetServices(),
    CarWashDataSeeder.GetCars(CarWashDataSeeder.GetClients()))), IOrderRepository;