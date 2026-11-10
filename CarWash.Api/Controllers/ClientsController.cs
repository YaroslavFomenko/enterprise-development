using CarWash.Application.Contracts.Dtos;
using CarWash.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Контроллер для управления клиентами автомойки.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService service, ILogger<ClientsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех клиентов.
    /// </summary>
    /// <returns>Коллекция клиентов.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<ClientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyCollection<ClientDto>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение списка клиентов.");
            return Ok(service.GetAll());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка клиентов.");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Возвращает клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>Клиент или 404, если не найден.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ClientDto> GetById(int id)
    {
        try
        {
            var client = service.GetById(id);
            if (client is null)
                return NotFound(new { message = $"Клиент с Id {id} не найден." });

            return Ok(client);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении клиента с Id {Id}.", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Создает нового клиента.
    /// </summary>
    /// <param name="dto">Данные нового клиента.</param>
    /// <returns>Созданный клиент.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ClientDto> Create(ClientCreateUpdateDto dto)
    {
        try
        {
            var created = service.Create(dto);
            logger.LogInformation("Создан клиент с Id {Id}.", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные данные при создании клиента.");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании клиента.");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Обновляет существующего клиента.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <param name="dto">Новые данные клиента.</param>
    /// <returns>Обновленный клиент.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ClientDto> Update(int id, ClientCreateUpdateDto dto)
    {
        try
        {
            var updated = service.Update(id, dto);
            logger.LogInformation("Обновлен клиент с Id {Id}.", id);
            return Ok(updated);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Клиент для обновления с Id {Id} не найден.", id);
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные данные при обновлении клиента с Id {Id}.", id);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении клиента с Id {Id}.", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Удаляет клиента.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>204 No Content.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            service.Delete(id);
            logger.LogInformation("Удален клиент с Id {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении клиента с Id {Id}.", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }
}