using CarWash.Domain.Entities;

namespace CarWash.Application.Contracts.Repositories;

/// <summary>
/// Репозиторий для работы с автомобилями.
/// </summary>
public interface ICarRepository : IRepository<Car>;