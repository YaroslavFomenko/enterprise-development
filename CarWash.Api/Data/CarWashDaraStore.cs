using CarWash.Domain.Data;
using CarWash.Domain.Entities;

namespace CarWash.Api.Data;

/// <summary>
///     Единое хранилище данных автомойки в памяти.
/// </summary>
public class CarWashDataStore
{
    /// <summary>
    ///     Инициализирует хранилище тестовыми данными.
    /// </summary>
    public CarWashDataStore()
    {
        Clients = CarWashDataSeeder.GetClients();
        Services = CarWashDataSeeder.GetServices();
        Cars = CarWashDataSeeder.GetCars(Clients);
        Orders = CarWashDataSeeder.GetOrders(Clients, Services, Cars);
    }

    /// <summary>Клиенты.</summary>
    public List<Client> Clients { get; }

    /// <summary>Услуги.</summary>
    public List<Service> Services { get; }

    /// <summary>Автомобили.</summary>
    public List<Car> Cars { get; }

    /// <summary>Заказы.</summary>
    public List<Order> Orders { get; }
}