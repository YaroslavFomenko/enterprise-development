using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Interfaces;

namespace CarWash.Api.Infrastructure.Repositories;

/// <summary>
/// Базовая реализация репозитория в памяти.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
public class InMemoryRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly List<T> _items;

    /// <summary>
    /// Инициализирует репозиторий переданной коллекцией.
    /// </summary>
    /// <param name="items">Коллекция сущностей.</param>
    protected InMemoryRepository(List<T> items)
    {
        _items = items;
    }

    /// <summary>
    /// Возвращает все сущности.
    /// </summary>
    /// <returns>Коллекция сущностей.</returns>
    public IReadOnlyCollection<T> GetAll()
    {
        return [.. _items];
    }

    /// <summary>
    /// Возвращает сущность по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <returns>Сущность или null, если не найдена.</returns>
    public T? GetById(int id)
    {
        return _items.FirstOrDefault(item => item.Id == id);
    }

    /// <summary>
    /// Добавляет новую сущность.
    /// </summary>
    /// <param name="entity">Сущность для добавления.</param>
    /// <returns>Добавленная сущность.</returns>
    public T Add(T entity)
    {
        var maxId = _items
            .Select(item => item.Id)
            .DefaultIfEmpty(0)
            .Max();
        entity.Id = maxId + 1;

        _items.Add(entity);
        return entity;
    }

    /// <summary>
    /// Обновляет существующую сущность.
    /// </summary>
    /// <param name="entity">Сущность с новыми данными.</param>
    /// <returns>Обновленная сущность.</returns>
    public T Update(T entity)
    {
        var existing = _items.FirstOrDefault(item => item.Id == entity.Id);
        if (existing != null)
        {
            var index = _items.IndexOf(existing);
            _items[index] = entity;
        }

        return entity;
    }

    /// <summary>
    /// Удаляет сущность по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <returns>True, если удаление успешно, иначе False.</returns>
    public bool Delete(int id)
    {
        var existing = _items.FirstOrDefault(item => item.Id == id);
        return existing != null && _items.Remove(existing);
    }
}