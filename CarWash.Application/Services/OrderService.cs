using CarWash.Application.Contracts.Dtos;
using CarWash.Application.Contracts.Interfaces;
using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Entities;

namespace CarWash.Application.Services;

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
    public async Task<IReadOnlyCollection<OrderDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetAllAsync(cancellationToken);
        return [.. orders.Select(ToDto)];
    }

    /// <summary>
    /// Возвращает заказ по идентификатору.
    /// </summary>
    public async Task<OrderDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(id, cancellationToken);
        return order is null ? null : ToDto(order);
    }

    /// <summary>
    /// Создает новый заказ.
    /// </summary>
    public async Task<OrderDto> CreateAsync(OrderCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var client = await clientRepository.GetByIdAsync(dto.ClientId, cancellationToken)
            ?? throw new ArgumentException($"Клиент с Id {dto.ClientId} не найден.");
        var car = await carRepository.GetByIdAsync(dto.CarId, cancellationToken)
            ?? throw new ArgumentException($"Автомобиль с Id {dto.CarId} не найден.");
        var service = await serviceRepository.GetByIdAsync(dto.ServiceId, cancellationToken)
            ?? throw new ArgumentException($"Услуга с Id {dto.ServiceId} не найдена.");

        var order = new Order
        {
            Client = client,
            Service = service,
            Car = car,
            StartTime = dto.StartTime,
            BoxNumber = dto.BoxNumber
        };

        var created = await orderRepository.AddAsync(order, cancellationToken);
        return ToDto(created);
    }

    /// <summary>
    /// Обновляет существующий заказ.
    /// </summary>
    public async Task<OrderDto> UpdateAsync(int id, OrderCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Заказ с Id {id} не найден.");

        var client = await clientRepository.GetByIdAsync(dto.ClientId, cancellationToken)
            ?? throw new ArgumentException($"Клиент с Id {dto.ClientId} не найден.");
        var car = await carRepository.GetByIdAsync(dto.CarId, cancellationToken)
            ?? throw new ArgumentException($"Автомобиль с Id {dto.CarId} не найден.");
        var service = await serviceRepository.GetByIdAsync(dto.ServiceId, cancellationToken)
            ?? throw new ArgumentException($"Услуга с Id {dto.ServiceId} не найдена.");

        order.Client = client;
        order.Service = service;
        order.Car = car;
        order.StartTime = dto.StartTime;
        order.BoxNumber = dto.BoxNumber;

        await orderRepository.UpdateAsync(order, cancellationToken);
        return ToDto(order);
    }

    /// <summary>
    /// Удаляет заказ.
    /// </summary>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return await orderRepository.DeleteAsync(id, cancellationToken);
    }

    private static OrderDto ToDto(Order o)
    {
        return new OrderDto(o.Id, o.Client.Id, o.Service.Id, o.Car.Id, o.StartTime, o.BoxNumber);
    }
}