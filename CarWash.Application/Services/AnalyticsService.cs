using CarWash.Application.Contracts.Dtos;
using CarWash.Application.Contracts.Interfaces;
using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Data;

namespace CarWash.Application.Services;

/// <summary>
/// Реализация сервиса для аналитических запросов.
/// </summary>
public class AnalyticsService(IOrderRepository orderRepository) : IAnalyticsService
{
    private readonly DateTime _today = CarWashDataSeeder.ReferenceDate.ToDateTime(TimeOnly.MinValue);

    /// <summary>
    /// Возвращает топ-5 клиентов по количеству посещений.
    /// </summary>
    public async Task<IReadOnlyCollection<TopClientDto>> GetTop5ClientsAsync(CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetAllAsync(cancellationToken);
        var result = orders
            .GroupBy(o => o.Client)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.FullName)
            .Take(5)
            .Select(g => new TopClientDto(g.Key.Id, g.Key.FullName, g.Key.Phone, g.Count()))
            .ToList();

        return result;
    }

    /// <summary>
    /// Возвращает автомобили, находящиеся на мойке в указанный момент.
    /// </summary>
    public async Task<IReadOnlyCollection<CarDto>> GetCarsInWashAsync(DateTime? moment, CancellationToken cancellationToken)
    {
        var checkMoment = moment ?? _today.AddHours(9).AddMinutes(5);
        var orders = await orderRepository.GetAllAsync(cancellationToken);

        var result = orders
            .Where(o => o.StartTime <= checkMoment
                        && checkMoment <= o.StartTime.AddMinutes(o.Service.DurationMinutes))
            .Select(o => new CarDto(o.Car.Id, o.Car.LicensePlate, o.Car.Brand, o.Car.Category, o.Car.ClientId))
            .ToList();

        return result;
    }

    /// <summary>
    /// Возвращает топ-5 наиболее популярных услуг.
    /// </summary>
    public async Task<IReadOnlyCollection<ServiceDto>> GetTop5ServicesAsync(CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetAllAsync(cancellationToken);
        var result = orders
            .GroupBy(o => o.Service)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.Name)
            .Take(5)
            .Select(g => new ServiceDto(g.Key.Id, g.Key.Name, g.Key.Category.ToString(), g.Key.Price, g.Key.DurationMinutes))
            .ToList();

        return result;
    }

    /// <summary>
    /// Возвращает время освобождения выбранного бокса мойки.
    /// </summary>
    public async Task<DateTime> GetBoxReleaseTimeAsync(int boxNumber, DateTime? fromMoment, CancellationToken cancellationToken)
    {
        var from = fromMoment ?? _today.AddHours(8);
        var orders = await orderRepository.GetAllAsync(cancellationToken);

        var result = orders
            .Where(o => o.BoxNumber == boxNumber && o.StartTime.AddMinutes(o.Service.DurationMinutes) >= from)
            .OrderByDescending(o => o.StartTime)
            .Select(o => o.StartTime.AddMinutes(o.Service.DurationMinutes))
            .FirstOrDefault();

        return result;
    }

    /// <summary>
    /// Возвращает суммарную выручку по каждой услуге.
    /// </summary>
    public async Task<IReadOnlyCollection<RevenueByServiceDto>> GetRevenueByServiceAsync(CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetAllAsync(cancellationToken);
        var result = orders
            .GroupBy(o => o.Service)
            .Select(g => new RevenueByServiceDto(
                new ServiceDto(g.Key.Id, g.Key.Name, g.Key.Category.ToString(), g.Key.Price, g.Key.DurationMinutes),
                g.Sum(o => o.Service.Price)))
            .ToList();

        return result;
    }
}