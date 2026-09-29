using CarWash.Domain.Enums;

namespace CarWash.Domain.Entities;

/// <summary>
/// Услуга автомойки.
/// </summary>
public class Service
{
    /// <summary>Идентификатор.</summary>
    public int Id { get; set; }

    /// <summary>Название услуги.</summary>
    public required string Name { get; set; }

    /// <summary>Категория автомобиля.</summary>
    public required CarCategory Category { get; set; }

    /// <summary>Стоимость услуги.</summary>
    public required decimal Price { get; set; }

    /// <summary>Длительность в минутах.</summary>
    public required int DurationMinutes { get; set; }
}