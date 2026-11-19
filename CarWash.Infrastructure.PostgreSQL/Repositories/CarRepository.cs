using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Entities;
using CarWash.Infrastructure.PostgreSQL.Data;

namespace CarWash.Infrastructure.PostgreSQL.Repositories;

/// <summary>
/// Репозиторий автомобилей в PostgreSQL.
/// </summary>
public class CarRepository(CarWashDbContext db) : PostgreSqlRepository<Car>(db), ICarRepository;