using System.ComponentModel.DataAnnotations;
using CarWash.Domain.Enums;

namespace CarWash.Application.Contracts.Dtos;

/// <summary>
/// DTO для создания или обновления автомобиля.
/// </summary>
/// <param name="LicensePlate">Государственный номер.</param>
/// <param name="Brand">Марка автомобиля.</param>
/// <param name="Category">Категория автомобиля.</param>
/// <param name="ClientId">Идентификатор владельца.</param>
public record CarCreateUpdateDto(
    [Required(ErrorMessage = "Госномер обязателен")]
    [StringLength(20, MinimumLength = 1)]
    string LicensePlate,
    [Required(ErrorMessage = "Марка обязательна")]
    [StringLength(50, MinimumLength = 1)]
    string Brand,
    [Required(ErrorMessage = "Категория обязательна")]
    CarCategory Category,
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный Id клиента")]
    int ClientId);