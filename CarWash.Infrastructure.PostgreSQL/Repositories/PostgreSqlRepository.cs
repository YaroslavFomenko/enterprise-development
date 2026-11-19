using CarWash.Application.Contracts.Repositories;
using CarWash.Infrastructure.PostgreSQL.Data;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.PostgreSQL.Repositories;

/// <summary>
/// Базовый репозиторий для работы с сущностями через EF Core.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
/// <param name="db">Контекст базы данных.</param>
public abstract class PostgreSqlRepository<T>(CarWashDbContext db) : IRepository<T>
    where T : class
{
    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    protected CarWashDbContext Database { get; } = db;

    /// <summary>
    /// Набор сущностей текущего типа.
    /// </summary>
    protected DbSet<T> Entities => Database.Set<T>();

    /// <summary>
    /// Возвращает все сущности.
    /// </summary>
    public virtual async Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await Entities.AsNoTracking().ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Возвращает сущность по идентификатору.
    /// </summary>
    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Entities.FindAsync([id], cancellationToken);
    }

    /// <summary>
    /// Добавляет новую сущность.
    /// </summary>
    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        Entities.Add(entity);
        await Database.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <summary>
    /// Обновляет существующую сущность.
    /// </summary>
    public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        var id = (int)typeof(T).GetProperty("Id")!.GetValue(entity)!;
        var current = await Entities.FindAsync([id], cancellationToken);
        if (current is null)
        {
            return false;
        }

        Database.Entry(current).CurrentValues.SetValues(entity);
        await Database.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Удаляет сущность по идентификатору.
    /// </summary>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await Entities.FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        Entities.Remove(entity);
        await Database.SaveChangesAsync(cancellationToken);
        return true;
    }
}