using CarWash.Api.Dtos;
using CarWash.Api.Interfaces;
using CarWash.Domain.Data;
using CarWash.Domain.Entities;
using CarWash.Domain.Enums;

namespace CarWash.Api.Services;

/// <summary>
///     Реализация сервиса для управления заказами.
/// </summary>
public class OrderService(IClientService clientService, ICarService carService, IServiceService serviceService)
    : IOrderService
{
    private readonly List<Order> _orders = CarWashDataSeeder.GetOrders(
        CarWashDataSeeder.GetClients(),
        CarWashDataSeeder.GetServices(),
        CarWashDataSeeder.GetCars(CarWashDataSeeder.GetClients()));

    /// <summary>
    ///     Возвращает список всех заказов.
    /// </summary>
    /// <returns>Коллекция заказов.</returns>
    public IReadOnlyCollection<OrderDto> GetAll()
    {
        return [.. _orders.Select(ToDto)];
    }

    /// <summary>
    ///     Возвращает заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>Заказ или null, если не найден.</returns>
    public OrderDto? GetById(int id)
    {
        Order? order = _orders.FirstOrDefault(o => o.Id == id);
        return order is null ? null : ToDto(order);
    }

    /// <summary>
    ///     Создает новый заказ.
    /// </summary>
    /// <param name="dto">Данные нового заказа.</param>
    /// <returns>Созданный заказ.</returns>
    public OrderDto Create(OrderCreateUpdateDto dto)
    {
        ClientDto? client = clientService.GetById(dto.ClientId);
        CarDto? car = carService.GetById(dto.CarId);
        ServiceDto? service = serviceService.GetById(dto.ServiceId);

        if (client is null)
        {
            throw new ArgumentException($"Клиент с Id {dto.ClientId} не найден.");
        }

        if (car is null)
        {
            throw new ArgumentException($"Автомобиль с Id {dto.CarId} не найден.");
        }

        if (service is null)
        {
            throw new ArgumentException($"Услуга с Id {dto.ServiceId} не найдена.");
        }

        var newId = _orders.Count > 0 ? _orders.Max(o => o.Id) + 1 : 1;

        var order = new Order
        {
            Id = newId,
            Client = new Client { Id = client.Id, FullName = client.FullName, Phone = client.Phone },
            Service = new Service
            {
                Id = service.Id,
                Name = service.Name,
                Category = ParseCategory(service.Category),
                Price = service.Price,
                DurationMinutes = service.DurationMinutes
            },
            Car = new Car
            {
                Id = car.Id, LicensePlate = car.LicensePlate, Brand = car.Brand, Category = car.Category,
                ClientId = car.ClientId
            },
            StartTime = dto.StartTime,
            BoxNumber = dto.BoxNumber
        };

        _orders.Add(order);
        return ToDto(order);
    }

    /// <summary>
    ///     Обновляет существующий заказ.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <param name="dto">Новые данные заказа.</param>
    /// <returns>Обновленный заказ.</returns>
    public OrderDto Update(int id, OrderCreateUpdateDto dto)
    {
        Order? order = _orders.FirstOrDefault(o => o.Id == id);
        if (order is null)
        {
            throw new KeyNotFoundException($"Заказ с Id {id} не найден.");
        }

        ClientDto? client = clientService.GetById(dto.ClientId);
        CarDto? car = carService.GetById(dto.CarId);
        ServiceDto? service = serviceService.GetById(dto.ServiceId);

        if (client is null)
        {
            throw new ArgumentException($"Клиент с Id {dto.ClientId} не найден.");
        }

        if (car is null)
        {
            throw new ArgumentException($"Автомобиль с Id {dto.CarId} не найден.");
        }

        if (service is null)
        {
            throw new ArgumentException($"Услуга с Id {dto.ServiceId} не найдена.");
        }

        order.Client = new Client { Id = client.Id, FullName = client.FullName, Phone = client.Phone };
        order.Service = new Service
        {
            Id = service.Id,
            Name = service.Name,
            Category = ParseCategory(service.Category),
            Price = service.Price,
            DurationMinutes = service.DurationMinutes
        };
        order.Car = new Car
        {
            Id = car.Id, LicensePlate = car.LicensePlate, Brand = car.Brand, Category = car.Category,
            ClientId = car.ClientId
        };
        order.StartTime = dto.StartTime;
        order.BoxNumber = dto.BoxNumber;

        return ToDto(order);
    }

    /// <summary>
    ///     Удаляет заказ.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>True, если удаление успешно, иначе False.</returns>
    public bool Delete(int id)
    {
        Order? order = _orders.FirstOrDefault(o => o.Id == id);
        if (order is null)
        {
            return false;
        }

        return _orders.Remove(order);
    }

    /// <summary>
    ///     Преобразует сущность заказа в DTO.
    /// </summary>
    /// <param name="o">Сущность заказа.</param>
    /// <returns>DTO заказа.</returns>
    private static OrderDto ToDto(Order o)
    {
        return new OrderDto(
            o.Id,
            o.Client.Id,
            o.Service.Id,
            o.Car.Id,
            o.StartTime,
            o.BoxNumber);
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