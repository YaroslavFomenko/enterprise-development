using CarWash.Api.Dtos;

namespace CarWash.Api.Interfaces;

/// <summary>
///     Сервис для управления клиентами автомойки.
/// </summary>
public interface IClientService
{
    /// <summary>
    ///     Возвращает список всех клиентов.
    /// </summary>
    /// <returns>Коллекция клиентов.</returns>
    public IReadOnlyCollection<ClientDto> GetAll();

    /// <summary>
    ///     Возвращает клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>Клиент или null, если не найден.</returns>
    public ClientDto? GetById(int id);

    /// <summary>
    ///     Создает нового клиента.
    /// </summary>
    /// <param name="dto">Данные нового клиента.</param>
    /// <returns>Созданный клиент.</returns>
    public ClientDto Create(ClientCreateUpdateDto dto);

    /// <summary>
    ///     Обновляет существующего клиента.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <param name="dto">Новые данные клиента.</param>
    /// <returns>Обновленный клиент.</returns>
    public ClientDto Update(int id, ClientCreateUpdateDto dto);

    /// <summary>
    ///     Удаляет клиента.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>True, если удаление успешно, иначе False.</returns>
    public bool Delete(int id);
}