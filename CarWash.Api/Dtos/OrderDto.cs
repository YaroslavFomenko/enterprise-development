using System.ComponentModel.DataAnnotations;

namespace CarWash.Api.Dtos;

/// <summary>
///     DTO для получения данных о заказе.
/// </summary>
/// <param name="Id">Идентификатор заказа.</param>
/// <param name="ClientId">Идентификатор клиента.</param>
/// <param name="ServiceId">Идентификатор услуги.</param>
/// <param name="CarId">Идентификатор автомобиля.</param>
/// <param name="StartTime">Дата и время начала обслуживания.</param>
/// <param name="BoxNumber">Номер бокса мойки.</param>
public record OrderDto(
    int Id,
    int ClientId,
    int ServiceId,
    int CarId,
    DateTime StartTime,
    int BoxNumber);

/// <summary>
///     DTO для создания или обновления заказа.
/// </summary>
/// <param name="ClientId">Идентификатор клиента.</param>
/// <param name="ServiceId">Идентификатор услуги.</param>
/// <param name="CarId">Идентификатор автомобиля.</param>
/// <param name="StartTime">Дата и время начала обслуживания.</param>
/// <param name="BoxNumber">Номер бокса мойки.</param>
public record OrderCreateUpdateDto(
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный Id клиента")]
    int ClientId,
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный Id услуги")]
    int ServiceId,
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный Id автомобиля")]
    int CarId,
    [Required(ErrorMessage = "Дата и время начала обязательны")]
    DateTime StartTime,
    [Range(1, 20, ErrorMessage = "Номер бокса должен быть от 1 до 20")]
    int BoxNumber);