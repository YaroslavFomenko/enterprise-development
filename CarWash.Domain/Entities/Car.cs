using CarWash.Domain.Enums;

namespace CarWash.Domain.Entities;

/// <summary>
/// Автомобиль клиента.
/// </summary>
public class Car
{
    /// <summary>Идентификатор.</summary>
    public int Id { get; set; }

    /// <summary>Государственный номер.</summary>
    public required string LicensePlate { get; set; }

    /// <summary>Марка автомобиля.</summary>
    public required string Brand { get; set; }

    /// <summary>Категория автомобиля.</summary>
    public required CarCategory Category { get; set; }

    /// <summary>Идентификатор владельца.</summary>
    public int ClientId { get; set; }

    /// <summary>Владелец автомобиля.</summary>
    public Client? Client { get; set; }
}