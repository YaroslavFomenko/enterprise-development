using CarWash.Api.Dtos;
using CarWash.Api.Interfaces;
using CarWash.Domain.Data;
using CarWash.Domain.Entities;

namespace CarWash.Api.Services;

/// <summary>
///     Реализация сервиса для управления клиентами.
/// </summary>
public class ClientService : IClientService
{
    private readonly List<Client> _clients;

    /// <summary>
    ///     Инициализирует сервис клиентов тестовыми данными.
    /// </summary>
    public ClientService()
    {
        _clients = CarWashDataSeeder.GetClients();
    }

    /// <summary>
    ///     Возвращает список всех клиентов.
    /// </summary>
    /// <returns>Коллекция клиентов.</returns>
    public IReadOnlyCollection<ClientDto> GetAll()
    {
        return [.. _clients.Select(ToDto)];
    }

    /// <summary>
    ///     Возвращает клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>Клиент или null, если не найден.</returns>
    public ClientDto? GetById(int id)
    {
        Client? client = _clients.FirstOrDefault(c => c.Id == id);
        return client is null ? null : ToDto(client);
    }

    /// <summary>
    ///     Создает нового клиента.
    /// </summary>
    /// <param name="dto">Данные нового клиента.</param>
    /// <returns>Созданный клиент.</returns>
    public ClientDto Create(ClientCreateUpdateDto dto)
    {
        var newId = _clients.Count > 0 ? _clients.Max(c => c.Id) + 1 : 1;

        var client = new Client
        {
            Id = newId,
            FullName = dto.FullName,
            Phone = dto.Phone
        };

        _clients.Add(client);
        return ToDto(client);
    }

    /// <summary>
    ///     Обновляет существующего клиента.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <param name="dto">Новые данные клиента.</param>
    /// <returns>Обновленный клиент.</returns>
    public ClientDto Update(int id, ClientCreateUpdateDto dto)
    {
        Client? client = _clients.FirstOrDefault(c => c.Id == id);
        if (client is null)
        {
            throw new KeyNotFoundException($"Клиент с Id {id} не найден.");
        }

        client.FullName = dto.FullName;
        client.Phone = dto.Phone;
        return ToDto(client);
    }

    /// <summary>
    ///     Удаляет клиента.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>True, если удаление успешно, иначе False.</returns>
    public bool Delete(int id)
    {
        Client? client = _clients.FirstOrDefault(c => c.Id == id);
        if (client is null)
        {
            return false;
        }

        return _clients.Remove(client);
    }

    /// <summary>
    ///     Преобразует сущность клиента в DTO.
    /// </summary>
    /// <param name="c">Сущность клиента.</param>
    /// <returns>DTO клиента.</returns>
    private static ClientDto ToDto(Client c)
    {
        return new ClientDto(c.Id, c.FullName, c.Phone);
    }
}