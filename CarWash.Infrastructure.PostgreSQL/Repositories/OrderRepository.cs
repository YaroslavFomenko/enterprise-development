using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Entities;
using CarWash.Infrastructure.PostgreSQL.Data;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.PostgreSQL.Repositories;

/// <summary>
/// Репозиторий заказов в PostgreSQL.
/// </summary>
public class OrderRepository(CarWashDbContext db) : PostgreSqlRepository<Order>(db), IOrderRepository
{
    /// <summary>
    /// Возвращает все заказы с подгруженными связями (Клиент, Услуга, Автомобиль).
    /// </summary>
    public override async Task<IReadOnlyCollection<Order>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await Entities
            .Include(o => o.Client) 
            .Include(o => o.Service) 
            .Include(o => o.Car)     
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}