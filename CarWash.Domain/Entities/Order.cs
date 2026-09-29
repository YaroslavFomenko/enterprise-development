namespace CarWash.Domain.Entities;

/// <summary>
/// Заказ на обслуживание автомобиля.
/// </summary>
public class Order
{
    /// <summary>Идентификатор.</summary>
    public int Id { get; set; }

    /// <summary>Клиент.</summary>
    public required Client Client { get; set; }

    /// <summary>Услуга.</summary>
    public required Service Service { get; set; }

    /// <summary>Автомобиль.</summary>
    public required Car Car { get; set; }

    /// <summary>Дата и время начала обслуживания.</summary>
    public required DateTime StartTime { get; set; }

    /// <summary>Номер бокса мойки.</summary>
    public required int BoxNumber { get; set; }
}