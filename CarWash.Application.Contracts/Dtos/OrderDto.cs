namespace CarWash.Application.Contracts.Dtos;

/// <summary>
/// DTO для получения данных о заказе.
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