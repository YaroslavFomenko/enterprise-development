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
    public IReadOnlyCollection<TDto> GetAll();

    /// <summary>
    /// Возвращает запись по идентификатору.
    /// </summary>
    public TDto? GetById(TKey id);

    /// <summary>
    /// Создает новую запись.
    /// </summary>
    public TDto Create(TCreateUpdateDto dto);

    /// <summary>
    /// Обновляет запись.
    /// </summary>
    public TDto Update(TKey id, TCreateUpdateDto dto);

    /// <summary>
    /// Удаляет запись.
    /// </summary>
    public bool Delete(TKey id);
}