namespace CarWash.Application.Contracts.Repositories;

/// <summary>
/// Общий интерфейс для работы с хранилищем сущностей.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Возвращает все сущности.
    /// </summary>
    public Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает сущность по идентификатору.
    /// </summary>
    public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет новую сущность.
    /// </summary>
    public Task<T> AddAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет сущность.
    /// </summary>
    public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет сущность.
    /// </summary>
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}