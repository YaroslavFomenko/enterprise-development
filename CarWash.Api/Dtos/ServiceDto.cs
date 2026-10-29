using System.ComponentModel.DataAnnotations;

namespace CarWash.Api.Dtos;

/// <summary>
///     DTO для получения данных об услуге.
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

/// <summary>
///     DTO для создания или обновления услуги.
/// </summary>
/// <param name="Name">Название услуги.</param>
/// <param name="Category">Категория автомобиля.</param>
/// <param name="Price">Стоимость услуги.</param>
/// <param name="DurationMinutes">Длительность услуги в минутах.</param>
public record ServiceCreateUpdateDto(
    [Required(ErrorMessage = "Название услуги обязательно")]
    [StringLength(100, MinimumLength = 1)]
    string Name,
    [Required(ErrorMessage = "Категория автомобиля обязательна")]
    string Category,
    [Range(0.01, 100000, ErrorMessage = "Цена должна быть больше 0")]
    decimal Price,
    [Range(1, 480, ErrorMessage = "Длительность должна быть от 1 до 480 минут")]
    int DurationMinutes);