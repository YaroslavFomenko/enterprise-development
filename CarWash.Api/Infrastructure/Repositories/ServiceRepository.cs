using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Data;
using CarWash.Domain.Entities;

namespace CarWash.Api.Infrastructure.Repositories;

/// <summary>
/// Репозиторий услуг в памяти.
/// </summary>
public class ServiceRepository() : InMemoryRepository<Service>(CarWashDataSeeder.GetServices()), IServiceRepository;