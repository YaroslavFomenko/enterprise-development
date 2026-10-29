using CarWash.Api.Dtos;

namespace CarWash.Api.Interfaces;

/// <summary>
///     Сервис для управления услугами автомойки.
/// </summary>
public interface IServiceService
{
    /// <summary>
    ///     Возвращает список всех услуг.
    /// </summary>
    /// <returns>Коллекция услуг.</returns>
    public IReadOnlyCollection<ServiceDto> GetAll();

    /// <summary>
    ///     Возвращает услугу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <returns>Услуга или null, если не найдена.</returns>
    public ServiceDto? GetById(int id);

    /// <summary>
    ///     Создает новую услугу.
    /// </summary>
    /// <param name="dto">Данные новой услуги.</param>
    /// <returns>Созданная услуга.</returns>
    public ServiceDto Create(ServiceCreateUpdateDto dto);

    /// <summary>
    ///     Обновляет существующую услугу.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <param name="dto">Новые данные услуги.</param>
    /// <returns>Обновленная услуга.</returns>
    public ServiceDto Update(int id, ServiceCreateUpdateDto dto);

    /// <summary>
    ///     Удаляет услугу.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <returns>True, если удаление успешно, иначе False.</returns>
    public bool Delete(int id);
}