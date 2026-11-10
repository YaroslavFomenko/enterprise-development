using CarWash.Application.Contracts.Dtos;
using CarWash.Application.Contracts.Interfaces;
using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Entities;

namespace CarWash.Api.Services;

/// <summary>
/// Реализация сервиса для управления автомобилями.
/// </summary>
public class CarService(ICarRepository carRepository, IClientRepository clientRepository) : ICarService
{
    /// <summary>
    /// Возвращает список всех автомобилей.
    /// </summary>
    /// <returns>Коллекция автомобилей.</returns>
    public IReadOnlyCollection<CarDto> GetAll()
    {
        return [.. carRepository.GetAll().Select(ToDto)];
    }

    /// <summary>
    /// Возвращает автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Автомобиль или null, если не найден.</returns>
    public CarDto? GetById(int id)
    {
        var car = carRepository.GetById(id);
        return car is null ? null : ToDto(car);
    }

    /// <summary>
    /// Создает новый автомобиль.
    /// </summary>
    /// <param name="dto">Данные нового автомобиля.</param>
    /// <returns>Созданный автомобиль.</returns>
    public CarDto Create(CarCreateUpdateDto dto)
    {
        var client = clientRepository.GetById(dto.ClientId) 
            ?? throw new ArgumentException($"Клиент с Id {dto.ClientId} не найден.");

        var car = new Car
        {
            LicensePlate = dto.LicensePlate,
            Brand = dto.Brand,
            Category = dto.Category,
            ClientId = dto.ClientId,
            Client = client
        };

        var created = carRepository.Add(car);
        return ToDto(created);
    }

    /// <summary>
    /// Обновляет существующий автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <param name="dto">Новые данные автомобиля.</param>
    /// <returns>Обновленный автомобиль.</returns>
    public CarDto Update(int id, CarCreateUpdateDto dto)
    {
        var car = carRepository.GetById(id) 
            ?? throw new KeyNotFoundException($"Автомобиль с Id {id} не найден.");

        var client = clientRepository.GetById(dto.ClientId) 
            ?? throw new ArgumentException($"Клиент с Id {dto.ClientId} не найден.");

        car.LicensePlate = dto.LicensePlate;
        car.Brand = dto.Brand;
        car.Category = dto.Category;
        car.ClientId = dto.ClientId;
        car.Client = client;

        carRepository.Update(car);
        return ToDto(car);
    }

    /// <summary>
    /// Удаляет автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>True, если удаление успешно, иначе False.</returns>
    public bool Delete(int id)
    {
        return carRepository.Delete(id);
    }

    /// <summary>
    /// Преобразует сущность автомобиля в DTO.
    /// </summary>
    /// <param name="c">Сущность автомобиля.</param>
    /// <returns>DTO автомобиля.</returns>
    private static CarDto ToDto(Car c)
    {
        return new CarDto(c.Id, c.LicensePlate, c.Brand, c.Category, c.ClientId);
    }
}