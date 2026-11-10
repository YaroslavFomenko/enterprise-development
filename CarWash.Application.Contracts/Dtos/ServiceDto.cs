using System.ComponentModel.DataAnnotations;

namespace CarWash.Application.Contracts.Dtos;

/// <summary>
/// DTO для получения данных об услуге.
/// </summary>
/// <param name="Id">Идентификатор услуги.</param>
/// <param name="Name">Название услуги.</param>
/// <param name="Category">Категория автомобиля.</param>
/// <param name="Price">Стоимость услуги.</param>
/// <param name="DurationMinutes">Длительность услуги в минутах.</param>
public record ServiceDto(
    int Id,
    string Name,
    string Category,
    decimal Price,
    int DurationMinutes);