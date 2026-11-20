namespace CarWash.Application.Contracts.Interfaces;

/// <summary>
/// Общий интерфейс для CRUD-операций.
/// </summary>
/// <typeparam name="TDto">DTO для чтения.</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO для создания и обновления.</typeparam>
/// <typeparam name="TKey">Тип идентификатора.</typeparam>
public interface IApplicationService<TDto, in TCreateUpdateDto, in TKey>
{
    /// <summary>
    /// Возвращает все записи.
    /// </summary>
    public Task<IReadOnlyCollection<TDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает запись по идентификатору.
    /// </summary>
    public Task<TDto?> GetByIdAsync(TKey id, CancellationToken cancellationToken);

    /// <summary>
    /// Создает новую запись.
    /// </summary>
    public Task<TDto> CreateAsync(TCreateUpdateDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет запись.
    /// </summary>
    public Task<TDto> UpdateAsync(TKey id, TCreateUpdateDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет запись.
    /// </summary>
    public Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken);
}