namespace CarWash.Application.Contracts.Dtos;

/// <summary>
/// DTO для представления выручки по услуге.
/// </summary>
/// <param name="Service">Услуга.</param>
/// <param name="Revenue">Суммарная выручка.</param>
public record RevenueByServiceDto(
    ServiceDto Service,
    decimal Revenue);