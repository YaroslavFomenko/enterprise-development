using CarWash.Application.Contracts.Dtos;
using CarWash.Application.Contracts.Interfaces;
using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Entities;

namespace CarWash.Api.Services;

/// <summary>
/// Реализация сервиса для управления заказами.
/// </summary>
public class OrderService(
    IOrderRepository orderRepository,
    IClientRepository clientRepository,
    ICarRepository carRepository,
    IServiceRepository serviceRepository) : IOrderService
{
    /// <summary>
    /// Возвращает список всех заказов.
    /// </summary>
    /// <returns>Коллекция заказов.</returns>
    public IReadOnlyCollection<OrderDto> GetAll()
    {
        return [.. orderRepository.GetAll().Select(ToDto)];
    }

    /// <summary>
    /// Возвращает заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>Заказ или null, если не найден.</returns>
    public OrderDto? GetById(int id)
    {
        var order = orderRepository.GetById(id);
        return order is null ? null : ToDto(order);
    }

    /// <summary>
    /// Создает новый заказ.
    /// </summary>
    /// <param name="dto">Данные нового заказа.</param>
    /// <returns>Созданный заказ.</returns>
    public OrderDto Create(OrderCreateUpdateDto dto)
    {
        var client = clientRepository.GetById(dto.ClientId) 
            ?? throw new ArgumentException($"Клиент с Id {dto.ClientId} не найден.");
        var car = carRepository.GetById(dto.CarId) 
            ?? throw new ArgumentException($"Автомобиль с Id {dto.CarId} не найден.");
        var service = serviceRepository.GetById(dto.ServiceId) 
            ?? throw new ArgumentException($"Услуга с Id {dto.ServiceId} не найдена.");

        var order = new Order
        {
            Client = client,
            Service = service,
            Car = car,
            StartTime = dto.StartTime,
            BoxNumber = dto.BoxNumber
        };

        var created = orderRepository.Add(order);
        return ToDto(created);
    }

    /// <summary>
    /// Обновляет существующий заказ.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <param name="dto">Новые данные заказа.</param>
    /// <returns>Обновленный заказ.</returns>
    public OrderDto Update(int id, OrderCreateUpdateDto dto)
    {
        var order = orderRepository.GetById(id) 
            ?? throw new KeyNotFoundException($"Заказ с Id {id} не найден.");

        var client = clientRepository.GetById(dto.ClientId) 
            ?? throw new ArgumentException($"Клиент с Id {dto.ClientId} не найден.");
        var car = carRepository.GetById(dto.CarId) 
            ?? throw new ArgumentException($"Автомобиль с Id {dto.CarId} не найден.");
        var service = serviceRepository.GetById(dto.ServiceId) 
            ?? throw new ArgumentException($"Услуга с Id {dto.ServiceId} не найдена.");

        order.Client = client;
        order.Service = service;
        order.Car = car;
        order.StartTime = dto.StartTime;
        order.BoxNumber = dto.BoxNumber;

        orderRepository.Update(order);
        return ToDto(order);
    }

    /// <summary>
    /// Удаляет заказ.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>True, если удаление успешно, иначе False.</returns>
    public bool Delete(int id)
    {
        return orderRepository.Delete(id);
    }

    /// <summary>
    /// Преобразует сущность заказа в DTO.
    /// </summary>
    /// <param name="o">Сущность заказа.</param>
    /// <returns>DTO заказа.</returns>
    private static OrderDto ToDto(Order o)
    {
        return new OrderDto(o.Id, o.Client.Id, o.Service.Id, o.Car.Id, o.StartTime, o.BoxNumber);
    }
}