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
    public IReadOnlyCollection<TopClientDto> GetTop5Clients();

    /// <summary>
    /// Автомобили на мойке в указанный момент.
    /// </summary>
    public IReadOnlyCollection<CarDto> GetCarsInWash(DateTime? moment);

    /// <summary>
    /// Топ-5 услуг.
    /// </summary>
    public IReadOnlyCollection<ServiceDto> GetTop5Services();

    /// <summary>
    /// Время освобождения бокса.
    /// </summary>
    public DateTime GetBoxReleaseTime(int boxNumber, DateTime? fromMoment);

    /// <summary>
    /// Выручка по услугам.
    /// </summary>
    public IReadOnlyCollection<RevenueByServiceDto> GetRevenueByService();
}