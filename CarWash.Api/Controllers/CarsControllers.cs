using CarWash.Api.Dtos;
using CarWash.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
///     Контроллер для управления автомобилями.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CarsController(ICarService carService, ILogger<CarsController> logger) : ControllerBase
{
    /// <summary>
    ///     Возвращает список всех автомобилей.
    /// </summary>
    /// <returns>Коллекция автомобилей.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<CarDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyCollection<CarDto>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение всех автомобилей.");
            return Ok(carService.GetAll());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка автомобилей.");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    ///     Возвращает автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Автомобиль или 404, если не найден.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CarDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<CarDto> GetById(int id)
    {
        try
        {
            CarDto? car = carService.GetById(id);
            if (car is null)
            {
                return NotFound(new { message = $"Автомобиль с Id {id} не найден." });
            }

            return Ok(car);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении автомобиля с Id {Id}.", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    ///     Создает новый автомобиль.
    /// </summary>
    /// <param name="dto">Данные нового автомобиля.</param>
    /// <returns>Созданный автомобиль.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CarDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<CarDto> Create(CarCreateUpdateDto dto)
    {
        try
        {
            CarDto created = carService.Create(dto);
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
    ///     Обновляет существующий автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <param name="dto">Новые данные автомобиля.</param>
    /// <returns>Обновленный автомобиль.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CarDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<CarDto> Update(int id, CarCreateUpdateDto dto)
    {
        try
        {
            CarDto updated = carService.Update(id, dto);
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
    ///     Удаляет автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>204, если удалено, иначе 404.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            var deleted = carService.Delete(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Автомобиль с Id {id} не найден." });
            }

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