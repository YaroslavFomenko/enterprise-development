using CarWash.Domain.Data;
using CarWash.Domain.Entities;

namespace CarWash.Tests;

/// <summary>
/// Фикстура с тестовыми данными автомойки.
/// </summary>
public class CarWashFixture
{
    /// <summary>Услуги.</summary>
    public List<Service> Services { get; }

    /// <summary>Клиенты.</summary>
    public List<Client> Clients { get; }

    /// <summary>Автомобили.</summary>
    public List<Car> Cars { get; }

    /// <summary>Заказы.</summary>
    public List<Order> Orders { get; }

    /// <summary>
    /// Инициализирует тестовые данные.
    /// </summary>
    public CarWashFixture()
    {
        Services = CarWashDataSeeder.GetServices();
        Clients = CarWashDataSeeder.GetClients();
        Cars = CarWashDataSeeder.GetCars(Clients);
        Orders = CarWashDataSeeder.GetOrders(Clients, Services, Cars);
    }
}