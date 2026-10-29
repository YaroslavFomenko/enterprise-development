using CarWash.Api.Data;
using CarWash.Api.Dtos;
using CarWash.Api.Interfaces;
using CarWash.Domain.Entities;

namespace CarWash.Api.Services;

/// <summary>
///     Реализация сервиса для управления заказами.
/// </summary>
public class OrderService(CarWashDataStore store) : IOrderService
{
    /// <summary>
    ///     Возвращает список всех заказов.
    /// </summary>
    /// <returns>Коллекция заказов.</returns>
    public IReadOnlyCollection<OrderDto> GetAll()
    {
        return [.. store.Orders.Select(ToDto)];
    }

    /// <summary>
    ///     Возвращает заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>Заказ или null, если не найден.</returns>
    public OrderDto? GetById(int id)
    {
        Order? order = store.Orders.FirstOrDefault(o => o.Id == id);
        return order is null ? null : ToDto(order);
    }

    /// <summary>
    ///     Создает новый заказ.
    /// </summary>
    /// <param name="dto">Данные нового заказа.</param>
    /// <returns>Созданный заказ.</returns>
    public OrderDto Create(OrderCreateUpdateDto dto)
    {
        Client? client = store.Clients.FirstOrDefault(c => c.Id == dto.ClientId);
        Car? car = store.Cars.FirstOrDefault(c => c.Id == dto.CarId);
        Service? service = store.Services.FirstOrDefault(s => s.Id == dto.ServiceId);

        if (client is null)
        {
            throw new ArgumentException($"Клиент с Id {dto.ClientId} не найден.");
        }

        if (car is null)
        {
            throw new ArgumentException($"Автомобиль с Id {dto.CarId} не найден.");
        }

        if (service is null)
        {
            throw new ArgumentException($"Услуга с Id {dto.ServiceId} не найдена.");
        }

        var newId = store.Orders.Count > 0 ? store.Orders.Max(o => o.Id) + 1 : 1;

        var order = new Order
        {
            Id = newId,
            Client = client,
            Service = service,
            Car = car,
            StartTime = dto.StartTime,
            BoxNumber = dto.BoxNumber
        };

        store.Orders.Add(order);
        return ToDto(order);
    }

    /// <summary>
    ///     Обновляет существующий заказ.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <param name="dto">Новые данные заказа.</param>
    /// <returns>Обновленный заказ.</returns>
    public OrderDto Update(int id, OrderCreateUpdateDto dto)
    {
        Order? order = store.Orders.FirstOrDefault(o => o.Id == id);
        if (order is null)
        {
            throw new KeyNotFoundException($"Заказ с Id {id} не найден.");
        }

        Client? client = store.Clients.FirstOrDefault(c => c.Id == dto.ClientId);
        Car? car = store.Cars.FirstOrDefault(c => c.Id == dto.CarId);
        Service? service = store.Services.FirstOrDefault(s => s.Id == dto.ServiceId);

        if (client is null)
        {
            throw new ArgumentException($"Клиент с Id {dto.ClientId} не найден.");
        }

        if (car is null)
        {
            throw new ArgumentException($"Автомобиль с Id {dto.CarId} не найден.");
        }

        if (service is null)
        {
            throw new ArgumentException($"Услуга с Id {dto.ServiceId} не найдена.");
        }

        order.Client = client;
        order.Service = service;
        order.Car = car;
        order.StartTime = dto.StartTime;
        order.BoxNumber = dto.BoxNumber;

        return ToDto(order);
    }

    /// <summary>
    ///     Удаляет заказ.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>True, если удаление успешно, иначе False.</returns>
    public bool Delete(int id)
    {
        Order? order = store.Orders.FirstOrDefault(o => o.Id == id);
        if (order is null)
        {
            return false;
        }

        return store.Orders.Remove(order);
    }

    /// <summary>
    ///     Преобразует сущность заказа в DTO.
    /// </summary>
    /// <param name="o">Сущность заказа.</param>
    /// <returns>DTO заказа.</returns>
    private static OrderDto ToDto(Order o)
    {
        return new OrderDto(o.Id, o.Client.Id, o.Service.Id, o.Car.Id, o.StartTime, o.BoxNumber);
    }
}