using CarWash.Application.Contracts.Dtos;

namespace CarWash.Application.Contracts.Interfaces;

/// <summary>
/// Сервис для управления клиентами.
/// </summary>
public interface IClientService : IApplicationService<ClientDto, ClientCreateUpdateDto, int>;