using CarWash.Domain.Interfaces;

namespace CarWash.Application.Contracts.Repositories;

/// <summary>
/// Общий интерфейс для работы с хранилищем сущностей.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
public interface IRepository<T> where T : class, IEntity
{
    /// <summary>
    /// Возвращает все сущности.
    /// </summary>
    public IReadOnlyCollection<T> GetAll();

    /// <summary>
    /// Возвращает сущность по идентификатору.
    /// </summary>
    public T? GetById(int id);

    /// <summary>
    /// Добавляет новую сущность.
    /// </summary>
    public T Add(T entity);

    /// <summary>
    /// Обновляет сущность.
    /// </summary>
    public T Update(T entity);

    /// <summary>
    /// Удаляет сущность.
    /// </summary>
    public bool Delete(int id);
}