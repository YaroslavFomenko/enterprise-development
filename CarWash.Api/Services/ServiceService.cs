using CarWash.Application.Contracts.Dtos;
using CarWash.Application.Contracts.Interfaces;
using CarWash.Application.Contracts.Repositories;
using CarWash.Domain.Entities;
using CarWash.Domain.Enums;

namespace CarWash.Api.Services;

/// <summary>
/// Реализация сервиса для управления услугами автомойки.
/// </summary>
public class ServiceService(IServiceRepository repository) : IServiceService
{
    /// <summary>
    /// Возвращает список всех услуг.
    /// </summary>
    /// <returns>Коллекция услуг.</returns>
    public IReadOnlyCollection<ServiceDto> GetAll()
    {
        return [.. repository.GetAll().Select(ToDto)];
    }

    /// <summary>
    /// Возвращает услугу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <returns>Услуга или null, если не найдена.</returns>
    public ServiceDto? GetById(int id)
    {
        var service = repository.GetById(id);
        return service is null ? null : ToDto(service);
    }

    /// <summary>
    /// Создает новую услугу.
    /// </summary>
    /// <param name="dto">Данные новой услуги.</param>
    /// <returns>Созданная услуга.</returns>
    public ServiceDto Create(ServiceCreateUpdateDto dto)
    {
        var service = new Service
        {
            Name = dto.Name,
            Category = ParseCategory(dto.Category),
            Price = dto.Price,
            DurationMinutes = dto.DurationMinutes
        };

        var created = repository.Add(service);
        return ToDto(created);
    }

    /// <summary>
    /// Обновляет существующую услугу.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <param name="dto">Новые данные услуги.</param>
    /// <returns>Обновленная услуга.</returns>
    public ServiceDto Update(int id, ServiceCreateUpdateDto dto)
    {
        var service = repository.GetById(id) 
            ?? throw new KeyNotFoundException($"Услуга с Id {id} не найдена.");

        service.Name = dto.Name;
        service.Category = ParseCategory(dto.Category);
        service.Price = dto.Price;
        service.DurationMinutes = dto.DurationMinutes;

        repository.Update(service);
        return ToDto(service);
    }

    /// <summary>
    /// Удаляет услугу.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <returns>True, если удаление успешно, иначе False.</returns>
    public bool Delete(int id)
    {
        return repository.Delete(id);
    }

    /// <summary>
    /// Преобразует сущность услуги в DTO.
    /// </summary>
    /// <param name="s">Сущность услуги.</param>
    /// <returns>DTO услуги.</returns>
    private static ServiceDto ToDto(Service s)
    {
        return new ServiceDto(s.Id, s.Name, s.Category.ToString(), s.Price, s.DurationMinutes);
    }

    /// <summary>
    /// Преобразует строковое представление категории в перечисление.
    /// </summary>
    /// <param name="category">Строковое представление категории.</param>
    /// <returns>Значение перечисления CarCategory.</returns>
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