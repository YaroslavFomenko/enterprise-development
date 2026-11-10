using CarWash.Application.Contracts.Dtos;

namespace CarWash.Application.Contracts.Interfaces;

/// <summary>
/// Сервис для управления услугами автомойки.
/// </summary>
public interface IServiceService : IApplicationService<ServiceDto, ServiceCreateUpdateDto, int>;