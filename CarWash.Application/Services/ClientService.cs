using CarWash.Application.Contracts.Dtos;
using CarWash.Application.Contracts.Interfaces;
using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Entities;

namespace CarWash.Application.Services;

/// <summary>
/// Реализация сервиса для управления клиентами.
/// </summary>
public class ClientService(IClientRepository repository) : IClientService
{
    /// <summary>
    /// Возвращает список всех клиентов.
    /// </summary>
    public async Task<IReadOnlyCollection<ClientDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var clients = await repository.GetAllAsync(cancellationToken);
        return [.. clients.Select(ToDto)];
    }

    /// <summary>
    /// Возвращает клиента по идентификатору.
    /// </summary>
    public async Task<ClientDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var client = await repository.GetByIdAsync(id, cancellationToken);
        return client is null ? null : ToDto(client);
    }

    /// <summary>
    /// Создает нового клиента.
    /// </summary>
    public async Task<ClientDto> CreateAsync(ClientCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var client = new Client
        {
            FullName = dto.FullName,
            Phone = dto.Phone
        };

        var created = await repository.AddAsync(client, cancellationToken);
        return ToDto(created);
    }

    /// <summary>
    /// Обновляет существующего клиента.
    /// </summary>
    public async Task<ClientDto> UpdateAsync(int id, ClientCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var client = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Клиент с Id {id} не найден.");

        client.FullName = dto.FullName;
        client.Phone = dto.Phone;

        await repository.UpdateAsync(client, cancellationToken);
        return ToDto(client);
    }

    /// <summary>
    /// Удаляет клиента.
    /// </summary>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return await repository.DeleteAsync(id, cancellationToken);
    }

    private static ClientDto ToDto(Client c)
    {
        return new ClientDto(c.Id, c.FullName, c.Phone);
    }
}