using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Entities;
using CarWash.Infrastructure.PostgreSQL.Data;

namespace CarWash.Infrastructure.PostgreSQL.Repositories;

/// <summary>
/// Репозиторий услуг в PostgreSQL.
/// </summary>
public class ServiceRepository(CarWashDbContext db) : PostgreSqlRepository<Service>(db), IServiceRepository;