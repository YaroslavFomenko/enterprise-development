using CarWash.Domain.Enums;

namespace CarWash.Application.Contracts.Dtos;

/// <summary>
/// DTO для получения данных об автомобиле.
/// </summary>
/// <param name="Id">Идентификатор автомобиля.</param>
/// <param name="LicensePlate">Государственный номер.</param>
/// <param name="Brand">Марка автомобиля.</param>
/// <param name="Category">Категория автомобиля.</param>
/// <param name="ClientId">Идентификатор владельца.</param>
public record CarDto(
    int Id,
    string LicensePlate,
    string Brand,
    CarCategory Category,
    int ClientId);