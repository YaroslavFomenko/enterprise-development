using CarWash.Api.Dtos;
using CarWash.Api.Interfaces;
using CarWash.Domain.Data;
using CarWash.Domain.Entities;

namespace CarWash.Api.Services;

/// <summary>
///     Реализация сервиса для управления автомобилями.
/// </summary>
public class CarService(IClientService clientService) : ICarService
{
    private readonly List<Car> _cars = CarWashDataSeeder.GetCars(CarWashDataSeeder.GetClients());

    /// <summary>
    ///     Возвращает список всех автомобилей.
    /// </summary>
    /// <returns>Коллекция автомобилей.</returns>
    public IReadOnlyCollection<CarDto> GetAll()
    {
        return [.. _cars.Select(ToDto)];
    }

    /// <summary>
    ///     Возвращает автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Автомобиль или null, если не найден.</returns>
    public CarDto? GetById(int id)
    {
        Car? car = _cars.FirstOrDefault(c => c.Id == id);
        return car is null ? null : ToDto(car);
    }

    /// <summary>
    ///     Создает новый автомобиль.
    /// </summary>
    /// <param name="dto">Данные нового автомобиля.</param>
    /// <returns>Созданный автомобиль.</returns>
    public CarDto Create(CarCreateUpdateDto dto)
    {
        ClientDto? client = clientService.GetById(dto.ClientId);
        if (client is null)
        {
            throw new ArgumentException($"Клиент с Id {dto.ClientId} не найден.");
        }

        var newId = _cars.Count > 0 ? _cars.Max(c => c.Id) + 1 : 1;
        var car = new Car
        {
            Id = newId,
            LicensePlate = dto.LicensePlate,
            Brand = dto.Brand,
            Category = dto.Category,
            ClientId = dto.ClientId
        };

        _cars.Add(car);
        return ToDto(car);
    }

    /// <summary>
    ///     Обновляет существующий автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <param name="dto">Новые данные автомобиля.</param>
    /// <returns>Обновленный автомобиль.</returns>
    public CarDto Update(int id, CarCreateUpdateDto dto)
    {
        Car? car = _cars.FirstOrDefault(c => c.Id == id);
        if (car is null)
        {
            throw new KeyNotFoundException($"Автомобиль с Id {id} не найден.");
        }

        ClientDto? client = clientService.GetById(dto.ClientId);
        if (client is null)
        {
            throw new ArgumentException($"Клиент с Id {dto.ClientId} не найден.");
        }

        car.LicensePlate = dto.LicensePlate;
        car.Brand = dto.Brand;
        car.Category = dto.Category;
        car.ClientId = dto.ClientId;

        return ToDto(car);
    }

    /// <summary>
    ///     Удаляет автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>True, если удаление успешно, иначе False.</returns>
    public bool Delete(int id)
    {
        Car? car = _cars.FirstOrDefault(c => c.Id == id);
        if (car is null)
        {
            return false;
        }

        return _cars.Remove(car);
    }

    /// <summary>
    ///     Преобразует сущность автомобиля в DTO.
    /// </summary>
    /// <param name="c">Сущность автомобиля.</param>
    /// <returns>DTO автомобиля.</returns>
    private static CarDto ToDto(Car c)
    {
        return new CarDto(c.Id, c.LicensePlate, c.Brand, c.Category, c.ClientId);
    }
}