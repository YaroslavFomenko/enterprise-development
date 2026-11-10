using CarWash.Application.Contracts.Dtos;
using CarWash.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Контроллер для управления услугами автомойки.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ServicesController(IServiceService service, ILogger<ServicesController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех услуг.
    /// </summary>
    /// <returns>Коллекция услуг.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<ServiceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyCollection<ServiceDto>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение всех услуг.");
            return Ok(service.GetAll());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка услуг.");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Возвращает услугу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <returns>Услуга или 404, если не найдена.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ServiceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ServiceDto> GetById(int id)
    {
        try
        {
            var serviceDto = service.GetById(id);
            if (serviceDto is null)
                return NotFound(new { message = $"Услуга с Id {id} не найдена." });

            return Ok(serviceDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении услуги с Id {Id}.", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Создает новую услугу.
    /// </summary>
    /// <param name="dto">Данные новой услуги.</param>
    /// <returns>Созданная услуга.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ServiceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ServiceDto> Create(ServiceCreateUpdateDto dto)
    {
        try
        {
            var created = service.Create(dto);
            logger.LogInformation("Создана услуга с Id {Id}.", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные данные при создании услуги.");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании услуги.");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Обновляет существующую услугу.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <param name="dto">Новые данные услуги.</param>
    /// <returns>Обновленная услуга.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ServiceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ServiceDto> Update(int id, ServiceCreateUpdateDto dto)
    {
        try
        {
            var updated = service.Update(id, dto);
            logger.LogInformation("Обновлена услуга с Id {Id}.", id);
            return Ok(updated);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не найдена услуга для обновления с Id {Id}.", id);
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные данные при обновлении услуги с Id {Id}.", id);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении услуги с Id {Id}.", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Удаляет услугу.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <returns>204 No Content.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            service.Delete(id);
            logger.LogInformation("Удалена услуга с Id {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении услуги с Id {Id}.", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }
}