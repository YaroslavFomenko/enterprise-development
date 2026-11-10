using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Data;
using CarWash.Domain.Entities;

namespace CarWash.Api.Infrastructure.Repositories;

/// <summary>
/// Репозиторий клиентов в памяти.
/// </summary>
public class ClientRepository() : InMemoryRepository<Client>(CarWashDataSeeder.GetClients()), IClientRepository;