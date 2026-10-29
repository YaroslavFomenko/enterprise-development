using CarWash.Api.Dtos;
using CarWash.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
///     Контроллер для управления заказами автомойки.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService, ILogger<OrdersController> logger) : ControllerBase
{
    /// <summary>
    ///     Возвращает список всех заказов.
    /// </summary>
    /// <returns>Коллекция заказов.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyCollection<OrderDto>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение всех заказов.");
            return Ok(orderService.GetAll());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка заказов.");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    ///     Возвращает заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>Заказ или 404, если не найден.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<OrderDto> GetById(int id)
    {
        try
        {
            OrderDto? order = orderService.GetById(id);
            if (order is null)
            {
                return NotFound(new { message = $"Заказ с Id {id} не найден." });
            }

            return Ok(order);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении заказа с Id {Id}.", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    ///     Создает новый заказ.
    /// </summary>
    /// <param name="dto">Данные нового заказа.</param>
    /// <returns>Созданный заказ.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<OrderDto> Create(OrderCreateUpdateDto dto)
    {
        try
        {
            OrderDto created = orderService.Create(dto);
            logger.LogInformation("Создан заказ с Id {Id}.", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные данные при создании заказа.");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании заказа.");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    ///     Обновляет существующий заказ.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <param name="dto">Новые данные заказа.</param>
    /// <returns>Обновленный заказ.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<OrderDto> Update(int id, OrderCreateUpdateDto dto)
    {
        try
        {
            OrderDto updated = orderService.Update(id, dto);
            logger.LogInformation("Обновлен заказ с Id {Id}.", id);
            return Ok(updated);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Заказ для обновления с Id {Id} не найден.", id);
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные данные при обновлении заказа с Id {Id}.", id);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении заказа с Id {Id}.", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    ///     Удаляет заказ.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>204, если удалено, иначе 404.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            var deleted = orderService.Delete(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Заказ с Id {id} не найден." });
            }

            logger.LogInformation("Удален заказ с Id {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении заказа с Id {Id}.", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера." });
        }
    }
}