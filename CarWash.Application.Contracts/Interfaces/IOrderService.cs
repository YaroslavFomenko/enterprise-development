using CarWash.Application.Contracts.Dtos;

namespace CarWash.Application.Contracts.Interfaces;

/// <summary>
/// Сервис для управления заказами.
/// </summary>
public interface IOrderService : IApplicationService<OrderDto, OrderCreateUpdateDto, int>;