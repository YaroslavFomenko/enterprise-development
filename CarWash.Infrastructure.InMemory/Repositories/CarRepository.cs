using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Data;
using CarWash.Domain.Entities;

namespace CarWash.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий автомобилей в памяти.
/// </summary>
public class CarRepository() : InMemoryRepository<Car>(CarWashDataSeeder.GetCars(CarWashDataSeeder.GetClients())), ICarRepository;