using CarWash.Application.Contracts.Dtos;

namespace CarWash.Application.Contracts.Interfaces;

/// <summary>
/// Сервис для управления автомобилями.
/// </summary>
public interface ICarService : IApplicationService<CarDto, CarCreateUpdateDto, int>;