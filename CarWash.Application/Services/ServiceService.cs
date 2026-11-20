using CarWash.Application.Contracts.Dtos;
using CarWash.Application.Contracts.Interfaces;
using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Entities;
using CarWash.Domain.Enums;

namespace CarWash.Application.Services;

/// <summary>
/// Реализация сервиса для управления услугами.
/// </summary>
public class ServiceService(IServiceRepository repository) : IServiceService
{
    /// <summary>
    /// Возвращает список всех услуг.
    /// </summary>
    public async Task<IReadOnlyCollection<ServiceDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var services = await repository.GetAllAsync(cancellationToken);
        return [.. services.Select(ToDto)];
    }

    /// <summary>
    /// Возвращает услугу по идентификатору.
    /// </summary>
    public async Task<ServiceDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var service = await repository.GetByIdAsync(id, cancellationToken);
        return service is null ? null : ToDto(service);
    }

    /// <summary>
    /// Создает новую услугу.
    /// </summary>
    public async Task<ServiceDto> CreateAsync(ServiceCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var service = new Service
        {
            Name = dto.Name,
            Category = ParseCategory(dto.Category),
            Price = dto.Price,
            DurationMinutes = dto.DurationMinutes
        };

        var created = await repository.AddAsync(service, cancellationToken);
        return ToDto(created);
    }

    /// <summary>
    /// Обновляет существующую услугу.
    /// </summary>
    public async Task<ServiceDto> UpdateAsync(int id, ServiceCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var service = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Услуга с Id {id} не найдена.");

        service.Name = dto.Name;
        service.Category = ParseCategory(dto.Category);
        service.Price = dto.Price;
        service.DurationMinutes = dto.DurationMinutes;

        await repository.UpdateAsync(service, cancellationToken);
        return ToDto(service);
    }

    /// <summary>
    /// Удаляет услугу.
    /// </summary>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return await repository.DeleteAsync(id, cancellationToken);
    }

    private static ServiceDto ToDto(Service s)
    {
        return new ServiceDto(s.Id, s.Name, s.Category.ToString(), s.Price, s.DurationMinutes);
    }

    private static CarCategory ParseCategory(string category)
    {
        if (!Enum.TryParse<CarCategory>(category, true, out var result)
            || !Enum.IsDefined(result))
        {
            throw new ArgumentException(
                "Недопустимая категория автомобиля. Используйте Passenger, Truck, Suv или Motorcycle.");
        }

        return result;
    }
}