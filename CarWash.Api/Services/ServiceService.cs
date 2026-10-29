using CarWash.Api.Dtos;
using CarWash.Api.Interfaces;
using CarWash.Domain.Data;
using CarWash.Domain.Entities;
using CarWash.Domain.Enums;

namespace CarWash.Api.Services;

/// <summary>
///     Реализация сервиса для управления услугами автомойки.
/// </summary>
public class ServiceService : IServiceService
{
    private readonly List<Service> _services;

    /// <summary>
    ///     Инициализирует сервис тестовыми данными.
    /// </summary>
    public ServiceService()
    {
        _services = CarWashDataSeeder.GetServices();
    }

    /// <summary>
    ///     Возвращает список всех услуг.
    /// </summary>
    /// <returns>Коллекция услуг.</returns>
    public IReadOnlyCollection<ServiceDto> GetAll()
    {
        return [.. _services.Select(ToDto)];
    }

    /// <summary>
    ///     Возвращает услугу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <returns>Услуга или null, если не найдена.</returns>
    public ServiceDto? GetById(int id)
    {
        Service? service = _services.FirstOrDefault(s => s.Id == id);
        return service is null ? null : ToDto(service);
    }

    /// <summary>
    ///     Создает новую услугу.
    /// </summary>
    /// <param name="dto">Данные новой услуги.</param>
    /// <returns>Созданная услуга.</returns>
    public ServiceDto Create(ServiceCreateUpdateDto dto)
    {
        CarCategory category = ParseCategory(dto.Category);
        var newId = _services.Count > 0 ? _services.Max(s => s.Id) + 1 : 1;

        var service = new Service
        {
            Id = newId,
            Name = dto.Name,
            Category = category,
            Price = dto.Price,
            DurationMinutes = dto.DurationMinutes
        };

        _services.Add(service);
        return ToDto(service);
    }

    /// <summary>
    ///     Обновляет существующую услугу.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <param name="dto">Новые данные услуги.</param>
    /// <returns>Обновленная услуга.</returns>
    public ServiceDto Update(int id, ServiceCreateUpdateDto dto)
    {
        Service? service = _services.FirstOrDefault(s => s.Id == id);
        if (service is null)
        {
            throw new KeyNotFoundException($"Услуга с Id {id} не найдена.");
        }

        service.Name = dto.Name;
        service.Category = ParseCategory(dto.Category);
        service.Price = dto.Price;
        service.DurationMinutes = dto.DurationMinutes;

        return ToDto(service);
    }

    /// <summary>
    ///     Удаляет услугу.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <returns>True, если удаление успешно, иначе False.</returns>
    public bool Delete(int id)
    {
        Service? service = _services.FirstOrDefault(s => s.Id == id);
        if (service is null)
        {
            return false;
        }

        return _services.Remove(service);
    }

    /// <summary>
    ///     Преобразует сущность услуги в DTO.
    /// </summary>
    /// <param name="s">Сущность услуги.</param>
    /// <returns>DTO услуги.</returns>
    private static ServiceDto ToDto(Service s)
    {
        return new ServiceDto(
            s.Id,
            s.Name,
            s.Category.ToString(),
            s.Price,
            s.DurationMinutes);
    }

    /// <summary>
    ///     Преобразует строковое представление категории в перечисление.
    /// </summary>
    /// <param name="category">Строковое представление категории.</param>
    /// <returns>Значение перечисления CarCategory.</returns>
    private static CarCategory ParseCategory(string category)
    {
        if (!Enum.TryParse<CarCategory>(category, true, out CarCategory result)
            || !Enum.IsDefined(result))
        {
            throw new ArgumentException(
                "Недопустимая категория автомобиля. Используйте Passenger, Truck, Suv или Motorcycle.");
        }

        return result;
    }
}