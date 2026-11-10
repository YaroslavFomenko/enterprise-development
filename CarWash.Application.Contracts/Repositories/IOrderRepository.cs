using CarWash.Domain.Entities;

namespace CarWash.Application.Contracts.Repositories;

/// <summary>
/// Репозиторий для работы с заказами.
/// </summary>
public interface IOrderRepository : IRepository<Order>;