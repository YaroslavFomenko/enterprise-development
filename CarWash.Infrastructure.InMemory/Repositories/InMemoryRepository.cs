using CarWash.Application.Contracts.Repositories;

namespace CarWash.Infrastructure.InMemory.Repositories;

/// <summary>
/// Базовая реализация репозитория в памяти.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
/// <param name="items">Коллекция сущностей.</param>
public abstract class InMemoryRepository<T>(List<T> items) : IRepository<T>
    where T : class
{
    /// <summary>
    /// Коллекция сущностей.
    /// </summary>
    protected List<T> Items { get; } = items;

    /// <summary>
    /// Возвращает все сущности.
    /// </summary>
    public Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyCollection<T>>([.. Items]);
    }

    /// <summary>
    /// Возвращает сущность по идентификатору.
    /// </summary>
    public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = Items.FirstOrDefault(item => (int)typeof(T).GetProperty("Id")!.GetValue(item)! == id);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Добавляет новую сущность.
    /// </summary>
    public Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        var idProperty = typeof(T).GetProperty("Id")!;
        var maxId = Items.Select(item => (int)idProperty.GetValue(item)!).DefaultIfEmpty(0).Max();
        idProperty.SetValue(entity, maxId + 1);

        Items.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновляет сущность.
    /// </summary>
    public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        var idProperty = typeof(T).GetProperty("Id")!;
        var id = (int)idProperty.GetValue(entity)!;
        var existing = Items.FirstOrDefault(item => (int)idProperty.GetValue(item)! == id);

        if (existing is null)
        {
            return Task.FromResult(false);
        }

        var index = Items.IndexOf(existing);
        Items[index] = entity;
        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет сущность по идентификатору.
    /// </summary>
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var idProperty = typeof(T).GetProperty("Id")!;
        var existing = Items.FirstOrDefault(item => (int)idProperty.GetValue(item)! == id);

        if (existing is null)
        {
            return Task.FromResult(false);
        }

        Items.Remove(existing);
        return Task.FromResult(true);
    }
}