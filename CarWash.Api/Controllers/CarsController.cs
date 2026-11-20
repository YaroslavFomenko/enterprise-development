using CarWash.Application.Contracts.Dtos;
using CarWash.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Контроллер для управления автомобилями.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CarsController(ICarService service, ILogger<CarsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех автомобилей.
    /// </summary>
    /// <param name="cancellationToken">Сигнал отмены запроса.</param>
    /// <returns>Коллекция автомобилей.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<CarDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyCollection<CarDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Запрос на получение всех автомобилей.");
            return Ok(await service.GetAllAsync(cancellationToken));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка автомобилей.");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Возвращает автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <param name="cancellationToken">Сигнал отмены запроса.</param>
    /// <returns>Автомобиль или 404, если не найден.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CarDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CarDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var car = await service.GetByIdAsync(id, cancellationToken);
            if (car is null)
                return NotFound(new { message = $"Автомобиль с Id {id} не найден." });

            return Ok(car);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении автомобиля с Id {Id}.", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Создает новый автомобиль.
    /// </summary>
    /// <param name="dto">Данные нового автомобиля.</param>
    /// <param name="cancellationToken">Сигнал отмены запроса.</param>
    /// <returns>Созданный автомобиль.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CarDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CarDto>> Create(CarCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var created = await service.CreateAsync(dto, cancellationToken);
            logger.LogInformation("Создан автомобиль с Id {Id}.", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные данные при создании автомобиля.");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании автомобиля.");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Обновляет существующий автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <param name="dto">Новые данные автомобиля.</param>
    /// <param name="cancellationToken">Сигнал отмены запроса.</param>
    /// <returns>Обновленный автомобиль.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CarDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CarDto>> Update(int id, CarCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await service.UpdateAsync(id, dto, cancellationToken);
            logger.LogInformation("Обновлен автомобиль с Id {Id}.", id);
            return Ok(updated);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Автомобиль для обновления с Id {Id} не найден.", id);
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные данные при обновлении автомобиля с Id {Id}.", id);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении автомобиля с Id {Id}.", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Удаляет автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <param name="cancellationToken">Сигнал отмены запроса.</param>
    /// <returns>204 No Content.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await service.DeleteAsync(id, cancellationToken);
            logger.LogInformation("Удален автомобиль с Id {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении автомобиля с Id {Id}.", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }
}