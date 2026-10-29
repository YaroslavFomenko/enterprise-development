using System.ComponentModel.DataAnnotations;

namespace CarWash.Api.Dtos;

/// <summary>
///     DTO для получения данных о клиенте.
/// </summary>
/// <param name="Id">Идентификатор клиента.</param>
/// <param name="FullName">ФИО клиента.</param>
/// <param name="Phone">Номер телефона.</param>
public record ClientDto(
    int Id,
    string FullName,
    string Phone);

/// <summary>
///     DTO для создания или обновления клиента.
/// </summary>
/// <param name="FullName">ФИО клиента.</param>
/// <param name="Phone">Номер телефона.</param>
public record ClientCreateUpdateDto(
    [Required(ErrorMessage = "ФИО обязательно")]
    [StringLength(150, MinimumLength = 1)]
    string FullName,
    [Required(ErrorMessage = "Телефон обязателен")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    string Phone);