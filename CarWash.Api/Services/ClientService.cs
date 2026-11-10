using CarWash.Application.Contracts.Dtos;
using CarWash.Application.Contracts.Interfaces;
using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Entities;

namespace CarWash.Api.Services;

/// <summary>
/// Реализация сервиса для управления клиентами.
/// </summary>
public class ClientService(IClientRepository repository) : IClientService
{
    /// <summary>
    /// Возвращает список всех клиентов.
    /// </summary>
    /// <returns>Коллекция клиентов.</returns>
    public IReadOnlyCollection<ClientDto> GetAll()
    {
        return [.. repository.GetAll().Select(ToDto)];
    }

    /// <summary>
    /// Возвращает клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>Клиент или null, если не найден.</returns>
    public ClientDto? GetById(int id)
    {
        var client = repository.GetById(id);
        return client is null ? null : ToDto(client);
    }

    /// <summary>
    /// Создает нового клиента.
    /// </summary>
    /// <param name="dto">Данные нового клиента.</param>
    /// <returns>Созданный клиент.</returns>
    public ClientDto Create(ClientCreateUpdateDto dto)
    {
        var client = new Client
        {
            FullName = dto.FullName,
            Phone = dto.Phone
        };

        var created = repository.Add(client);
        return ToDto(created);
    }

    /// <summary>
    /// Обновляет существующего клиента.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <param name="dto">Новые данные клиента.</param>
    /// <returns>Обновленный клиент.</returns>
    public ClientDto Update(int id, ClientCreateUpdateDto dto)
    {
        var client = repository.GetById(id) 
            ?? throw new KeyNotFoundException($"Клиент с Id {id} не найден.");

        client.FullName = dto.FullName;
        client.Phone = dto.Phone;

        repository.Update(client);
        return ToDto(client);
    }

    /// <summary>
    /// Удаляет клиента.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>True, если удаление успешно, иначе False.</returns>
    public bool Delete(int id)
    {
        return repository.Delete(id);
    }

    /// <summary>
    /// Преобразует сущность клиента в DTO.
    /// </summary>
    /// <param name="c">Сущность клиента.</param>
    /// <returns>DTO клиента.</returns>
    private static ClientDto ToDto(Client c)
    {
        return new ClientDto(c.Id, c.FullName, c.Phone);
    }
}