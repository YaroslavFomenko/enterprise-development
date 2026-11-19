using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Entities;
using CarWash.Infrastructure.PostgreSQL.Data;

namespace CarWash.Infrastructure.PostgreSQL.Repositories;

/// <summary>
/// Репозиторий клиентов в PostgreSQL.
/// </summary>
public class ClientRepository(CarWashDbContext db) : PostgreSqlRepository<Client>(db), IClientRepository;