using CarWash.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Контроллер для выполнения аналитических запросов.
/// </summary>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    /// <summary>
    /// Возвращает топ-5 клиентов по количеству посещений.
    /// </summary>
    /// <param name="cancellationToken">Сигнал отмены запроса.</param>
    /// <returns>Список клиентов.</returns>
    [HttpGet("top-clients")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTop5Clients(CancellationToken cancellationToken)
    {
        return Ok(await analyticsService.GetTop5ClientsAsync(cancellationToken));
    }

    /// <summary>
    /// Возвращает список автомобилей, находящихся на мойке в указанный момент.
    /// </summary>
    /// <param name="moment">Момент времени для проверки.</param>
    /// <param name="cancellationToken">Сигнал отмены запроса.</param>
    /// <returns>Список автомобилей.</returns>
    [HttpGet("cars-in-wash")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCarsInWash([FromQuery] DateTime? moment, CancellationToken cancellationToken)
    {
        return Ok(await analyticsService.GetCarsInWashAsync(moment, cancellationToken));
    }

    /// <summary>
    /// Возвращает топ-5 наиболее популярных услуг.
    /// </summary>
    /// <param name="cancellationToken">Сигнал отмены запроса.</param>
    /// <returns>Список услуг.</returns>
    [HttpGet("top-services")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTop5Services(CancellationToken cancellationToken)
    {
        return Ok(await analyticsService.GetTop5ServicesAsync(cancellationToken));
    }

    /// <summary>
    /// Возвращает время освобождения выбранного бокса мойки.
    /// </summary>
    /// <param name="boxNumber">Номер бокса.</param>
    /// <param name="fromMoment">Начало отсчета.</param>
    /// <param name="cancellationToken">Сигнал отмены запроса.</param>
    /// <returns>Дата и время освобождения бокса.</returns>
    [HttpGet("box-release-time")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBoxReleaseTime(
        [FromQuery] int boxNumber,
        [FromQuery] DateTime? fromMoment,
        CancellationToken cancellationToken)
    {
        var result = await analyticsService.GetBoxReleaseTimeAsync(boxNumber, fromMoment, cancellationToken);
        if (result == default)
        {
            return NotFound(new { message = $"Для бокса {boxNumber} не найдено заказов." });
        }

        return Ok(result);
    }

    /// <summary>
    /// Возвращает суммарную выручку по каждой услуге.
    /// </summary>
    /// <param name="cancellationToken">Сигнал отмены запроса.</param>
    /// <returns>Список услуг с выручкой.</returns>
    [HttpGet("revenue-by-service")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRevenueByService(CancellationToken cancellationToken)
    {
        return Ok(await analyticsService.GetRevenueByServiceAsync(cancellationToken));
    }
}