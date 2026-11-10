namespace CarWash.Application.Contracts.Dtos;

/// <summary>
/// DTO для получения данных о клиенте.
/// </summary>
/// <param name="Id">Идентификатор клиента.</param>
/// <param name="FullName">ФИО клиента.</param>
/// <param name="Phone">Номер телефона.</param>
public record ClientDto(
    int Id,
    string FullName,
    string Phone);