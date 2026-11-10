namespace CarWash.Application.Contracts.Dtos;

/// <summary>
/// DTO для представления клиента в топе посещений.
/// </summary>
/// <param name="Id">Идентификатор клиента.</param>
/// <param name="FullName">ФИО клиента.</param>
/// <param name="Phone">Номер телефона.</param>
/// <param name="VisitCount">Количество посещений.</param>
public record TopClientDto(
    int Id,
    string FullName,
    string Phone,
    int VisitCount);