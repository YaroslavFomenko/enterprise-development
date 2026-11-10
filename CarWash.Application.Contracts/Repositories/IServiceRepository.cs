using CarWash.Domain.Entities;

namespace CarWash.Application.Contracts.Repositories;

/// <summary>
/// Репозиторий для работы с услугами.
/// </summary>
public interface IServiceRepository : IRepository<Service>;