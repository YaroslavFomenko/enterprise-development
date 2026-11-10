using CarWash.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Контроллер для выполнения аналитических запросов по данным автомойки.
/// </summary>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    /// <summary>
    /// Возвращает топ-5 клиентов по количеству посещений.
    /// </summary>
    /// <returns>Список клиентов.</returns>
    [HttpGet("top-clients")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetTop5Clients()
    {
        return Ok(analyticsService.GetTop5Clients());
    }

    /// <summary>
    /// Возвращает список автомобилей, находящихся на мойке в указанный момент.
    /// </summary>
    /// <param name="moment">Момент времени для проверки.</param>
    /// <returns>Список автомобилей.</returns>
    [HttpGet("cars-in-wash")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetCarsInWash([FromQuery] DateTime? moment)
    {
        return Ok(analyticsService.GetCarsInWash(moment));
    }

    /// <summary>
    /// Возвращает топ-5 наиболее популярных услуг.
    /// </summary>
    /// <returns>Список услуг.</returns>
    [HttpGet("top-services")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetTop5Services()
    {
        return Ok(analyticsService.GetTop5Services());
    }

    /// <summary>
    /// Возвращает время освобождения выбранного бокса мойки.
    /// </summary>
    /// <param name="boxNumber">Номер бокса.</param>
    /// <param name="fromMoment">Начало отсчета.</param>
    /// <returns>Дата и время освобождения бокса.</returns>
    [HttpGet("box-release-time")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetBoxReleaseTime(
        [FromQuery] int boxNumber,
        [FromQuery] DateTime? fromMoment)
    {
        var result = analyticsService.GetBoxReleaseTime(boxNumber, fromMoment);
        if (result == default)
        {
            return NotFound(new { message = $"Для бокса {boxNumber} не найдено заказов." });
        }

        return Ok(result);
    }

    /// <summary>
    /// Возвращает суммарную выручку по каждой услуге.
    /// </summary>
    /// <returns>Список услуг с выручкой.</returns>
    [HttpGet("revenue-by-service")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetRevenueByService()
    {
        return Ok(analyticsService.GetRevenueByService());
    }
}