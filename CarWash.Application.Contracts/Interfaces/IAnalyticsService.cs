using CarWash.Application.Contracts.Dtos;

namespace CarWash.Application.Contracts.Interfaces;

/// <summary>
/// Сервис для аналитических запросов.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Топ-5 клиентов по посещениям.
    /// </summary>
    public Task<IReadOnlyCollection<TopClientDto>> GetTop5ClientsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Автомобили на мойке в указанный момент.
    /// </summary>
    public Task<IReadOnlyCollection<CarDto>> GetCarsInWashAsync(DateTime? moment, CancellationToken cancellationToken);

    /// <summary>
    /// Топ-5 услуг.
    /// </summary>
    public Task<IReadOnlyCollection<ServiceDto>> GetTop5ServicesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Время освобождения бокса.
    /// </summary>
    public Task<DateTime> GetBoxReleaseTimeAsync(int boxNumber, DateTime? fromMoment, CancellationToken cancellationToken);

    /// <summary>
    /// Выручка по услугам.
    /// </summary>
    public Task<IReadOnlyCollection<RevenueByServiceDto>> GetRevenueByServiceAsync(CancellationToken cancellationToken);
}