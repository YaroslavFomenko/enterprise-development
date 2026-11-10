using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Data;
using CarWash.Domain.Entities;

namespace CarWash.Api.Infrastructure.Repositories;

/// <summary>
/// Репозиторий автомобилей в памяти.
/// </summary>
public class CarRepository() : InMemoryRepository<Car>(CarWashDataSeeder.GetCars(CarWashDataSeeder.GetClients())), ICarRepository;