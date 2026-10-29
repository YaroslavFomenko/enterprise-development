using CarWash.Domain.Data;
using CarWash.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
///     Контроллер для выполнения аналитических запросов по данным автомойки.
/// </summary>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController : ControllerBase
{
    private readonly List<Order> _orders;
    private readonly DateTime _today;

    /// <summary>
    ///     Инициализирует контроллер тестовыми данными для анализа.
    /// </summary>
    public AnalyticsController()
    {
        List<Client> clients = CarWashDataSeeder.GetClients();
        List<Service> services = CarWashDataSeeder.GetServices();
        List<Car> cars = CarWashDataSeeder.GetCars(clients);
        _orders = CarWashDataSeeder.GetOrders(clients, services, cars);
        _today = CarWashDataSeeder.ReferenceDate.ToDateTime(TimeOnly.MinValue);
    }

    /// <summary>
    ///     Возвращает топ-5 клиентов по количеству посещений.
    /// </summary>
    /// <returns>Список ФИО клиентов.</returns>
    [HttpGet("top-clients")]
    [ProducesResponseType(typeof(IReadOnlyCollection<string>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyCollection<string>> GetTop5Clients()
    {
        var result = _orders
            .GroupBy(o => o.Client)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.FullName)
            .Take(5)
            .Select(g => g.Key.FullName)
            .ToList();

        return Ok(result);
    }

    /// <summary>
    ///     Возвращает список автомобилей, находящихся на мойке в указанный момент.
    /// </summary>
    /// <param name="moment">Момент времени для проверки. Если не указан, используется опорное время из сидера.</param>
    /// <returns>Список государственных номеров автомобилей.</returns>
    [HttpGet("cars-in-wash")]
    [ProducesResponseType(typeof(IReadOnlyCollection<string>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyCollection<string>> GetCarsInWash([FromQuery] DateTime? moment)
    {
        DateTime checkMoment = moment ?? _today.AddHours(9).AddMinutes(5);

        var result = _orders
            .Where(o => o.StartTime <= checkMoment
                        && checkMoment < o.StartTime.AddMinutes(o.Service.DurationMinutes))
            .Select(o => o.Car.LicensePlate)
            .ToList();

        return Ok(result);
    }

    /// <summary>
    ///     Возвращает топ-5 наиболее популярных услуг.
    /// </summary>
    /// <returns>Список названий услуг.</returns>
    [HttpGet("top-services")]
    [ProducesResponseType(typeof(IReadOnlyCollection<string>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyCollection<string>> GetTop5Services()
    {
        var result = _orders
            .GroupBy(o => o.Service)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.Name)
            .Take(5)
            .Select(g => g.Key.Name)
            .ToList();

        return Ok(result);
    }

    /// <summary>
    ///     Возвращает время освобождения выбранного бокса мойки.
    /// </summary>
    /// <param name="boxNumber">Номер бокса.</param>
    /// <param name="fromMoment">Начало отсчета. Если не указано, используется опорное время из сидера.</param>
    /// <returns>Дата и время освобождения бокса.</returns>
    [HttpGet("box-release-time")]
    [ProducesResponseType(typeof(DateTime), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DateTime> GetBoxReleaseTime(
        [FromQuery] int boxNumber,
        [FromQuery] DateTime? fromMoment)
    {
        DateTime from = fromMoment ?? _today.AddHours(8);

        DateTime result = _orders
            .Where(o => o.BoxNumber == boxNumber && o.StartTime >= from)
            .OrderByDescending(o => o.StartTime)
            .Select(o => o.StartTime.AddMinutes(o.Service.DurationMinutes))
            .FirstOrDefault();

        if (result == default)
        {
            return NotFound(new { message = $"Для бокса {boxNumber} не найдено заказов начиная с {from}." });
        }

        return Ok(result);
    }

    /// <summary>
    ///     Возвращает суммарную выручку по каждой услуге.
    /// </summary>
    /// <returns>Словарь, где ключ — название услуги, значение — суммарная выручка.</returns>
    [HttpGet("revenue-by-service")]
    [ProducesResponseType(typeof(Dictionary<string, decimal>), StatusCodes.Status200OK)]
    public ActionResult<Dictionary<string, decimal>> GetRevenueByService()
    {
        var result = _orders
            .GroupBy(o => o.Service.Name)
            .ToDictionary(g => g.Key, g => g.Sum(o => o.Service.Price));

        return Ok(result);
    }
}