using CarWash.Application.Contracts.Dtos;
using CarWash.Application.Contracts.Interfaces;
using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Entities;

namespace CarWash.Application.Services;

/// <summary>
/// Реализация сервиса для управления автомобилями.
/// </summary>
public class CarService(ICarRepository carRepository, IClientRepository clientRepository) : ICarService
{
    /// <summary>
    /// Возвращает список всех автомобилей.
    /// </summary>
    public async Task<IReadOnlyCollection<CarDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var cars = await carRepository.GetAllAsync(cancellationToken);
        return [.. cars.Select(ToDto)];
    }

    /// <summary>
    /// Возвращает автомобиль по идентификатору.
    /// </summary>
    public async Task<CarDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var car = await carRepository.GetByIdAsync(id, cancellationToken);
        return car is null ? null : ToDto(car);
    }

    /// <summary>
    /// Создает новый автомобиль.
    /// </summary>
    public async Task<CarDto> CreateAsync(CarCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var client = await clientRepository.GetByIdAsync(dto.ClientId, cancellationToken)
            ?? throw new ArgumentException($"Клиент с Id {dto.ClientId} не найден.");

        var car = new Car
        {
            LicensePlate = dto.LicensePlate,
            Brand = dto.Brand,
            Category = dto.Category,
            ClientId = dto.ClientId,
            Client = client
        };

        var created = await carRepository.AddAsync(car, cancellationToken);
        return ToDto(created);
    }

    /// <summary>
    /// Обновляет существующий автомобиль.
    /// </summary>
    public async Task<CarDto> UpdateAsync(int id, CarCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var car = await carRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Автомобиль с Id {id} не найден.");

        var client = await clientRepository.GetByIdAsync(dto.ClientId, cancellationToken)
            ?? throw new ArgumentException($"Клиент с Id {dto.ClientId} не найден.");

        car.LicensePlate = dto.LicensePlate;
        car.Brand = dto.Brand;
        car.Category = dto.Category;
        car.ClientId = dto.ClientId;
        car.Client = client;

        await carRepository.UpdateAsync(car, cancellationToken);
        return ToDto(car);
    }

    /// <summary>
    /// Удаляет автомобиль.
    /// </summary>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return await carRepository.DeleteAsync(id, cancellationToken);
    }

    private static CarDto ToDto(Car c)
    {
        return new CarDto(c.Id, c.LicensePlate, c.Brand, c.Category, c.ClientId);
    }
}