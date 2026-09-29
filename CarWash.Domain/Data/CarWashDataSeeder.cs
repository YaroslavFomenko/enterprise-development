using CarWash.Domain.Entities;
using CarWash.Domain.Enums;

namespace CarWash.Domain.Data;

/// <summary>
/// Генератор тестовых данных для автомойки.
/// </summary>
public static class CarWashDataSeeder
{
    /// <summary>
    /// Создаёт список услуг.
    /// </summary>
    public static List<Service> GetServices()
    {
        return
        [
            new Service()
            {
                Id = 1,
                Name = "Экспресс-мойка",
                Category = CarCategory.Passenger,
                Price = 300,
                DurationMinutes = 15
            },
            new Service()
            {
                Id = 2,
                Name = "Комплексная мойка",
                Category = CarCategory.Passenger,
                Price = 600,
                DurationMinutes = 40
            },
            new Service()
            {
                Id = 3,
                Name = "Химчистка салона",
                Category = CarCategory.Passenger,
                Price = 2500,
                DurationMinutes = 120
            },
            new Service()
            {
                Id = 4,
                Name = "Мойка грузовика",
                Category = CarCategory.Truck,
                Price = 1500,
                DurationMinutes = 60
            },
            new Service()
            {
                Id = 5,
                Name = "Полировка кузова",
                Category = CarCategory.Passenger,
                Price = 3000,
                DurationMinutes = 90
            },
            new Service()
            {
                Id = 6,
                Name = "Мойка внедорожника",
                Category = CarCategory.Suv,
                Price = 800,
                DurationMinutes = 45
            },
            new Service()
            {
                Id = 7,
                Name = "Чернение резины",
                Category = CarCategory.Passenger,
                Price = 200,
                DurationMinutes = 10
            },
            new Service()
            {
                Id = 8,
                Name = "Мойка мотоцикла",
                Category = CarCategory.Motorcycle,
                Price = 400,
                DurationMinutes = 20
            },
            new Service()
            {
                Id = 9,
                Name = "Нанесение воска",
                Category = CarCategory.Passenger,
                Price = 1200,
                DurationMinutes = 50
            },
            new Service()
            {
                Id = 10,
                Name = "Уборка багажника",
                Category = CarCategory.Passenger,
                Price = 500,
                DurationMinutes = 25
            },
        ];
    }

    /// <summary>
    /// Создаёт список клиентов.
    /// </summary>
    public static List<Client> GetClients()
    {
        return
        [
            new Client()
            {
                Id = 1,
                FullName = "Иванов Иван Иванович",
                Phone = "+7 (911) 100-00-01"
            },
            new Client()
            {
                Id = 2,
                FullName = "Петров Пётр Петрович",
                Phone = "+7 (911) 100-00-02"
            },
            new Client()
            {
                Id = 3,
                FullName = "Сидорова Анна Сергеевна",
                Phone = "+7 (911) 100-00-03"
            },
            new Client()
            {
                Id = 4,
                FullName = "Кузнецов Дмитрий Олегович",
                Phone = "+7 (911) 100-00-04"
            },
            new Client()
            {
                Id = 5,
                FullName = "Смирнова Елена Владимировна",
                Phone = "+7 (911) 100-00-05"
            },
            new Client()
            {
                Id = 6,
                FullName = "Новиков Артём Игоревич",
                Phone = "+7 (911) 100-00-06"
            },
            new Client()
            {
                Id = 7,
                FullName = "Морозова Ольга Николаевна",
                Phone = "+7 (911) 100-00-07"
            },
            new Client()
            {
                Id = 8,
                FullName = "Волков Сергей Андреевич",
                Phone = "+7 (911) 100-00-08"
            },
            new Client()
            {
                Id = 9,
                FullName = "Зайцева Татьяна Львовна",
                Phone = "+7 (911) 100-00-09"
            },
            new Client()
            {
                Id = 10,
                FullName = "Лебедев Максим Викторович",
                Phone = "+7 (911) 100-00-10"
            },
            new Client()
            {
                Id = 11,
                FullName = "Соколова Ирина Дмитриевна",
                Phone = "+7 (911) 100-00-11"
            },
            new Client()
            {
                Id = 12,
                FullName = "Павлов Роман Юрьевич",
                Phone = "+7 (911) 100-00-12"
            },
        ];
    }

    /// <summary>
    /// Создаёт список автомобилей.
    /// </summary>
    public static List<Car> GetCars(List<Client> clients)
    {
        return
        [
            new Car()
            {
                Id = 1,
                LicensePlate = "А123ВС77",
                Brand = "Toyota Camry",
                Category = CarCategory.Passenger,
                ClientId = 1,
                Client = clients[0]
            },
            new Car()
            {
                Id = 2,
                LicensePlate = "В456ЕК99",
                Brand = "Kia Rio",
                Category = CarCategory.Passenger,
                ClientId = 2,
                Client = clients[1]
            },
            new Car()
            {
                Id = 3,
                LicensePlate = "С789МН50",
                Brand = "BMW X5",
                Category = CarCategory.Suv,
                ClientId = 3,
                Client = clients[2]
            },
            new Car()
            {
                Id = 4,
                LicensePlate = "Е012ОР78",
                Brand = "Volvo FH",
                Category = CarCategory.Truck,
                ClientId = 4,
                Client = clients[3]
            },
            new Car()
            {
                Id = 5,
                LicensePlate = "К345ТУ47",
                Brand = "Hyundai Solaris",
                Category = CarCategory.Passenger,
                ClientId = 5,
                Client = clients[4]
            },
            new Car()
            {
                Id = 6,
                LicensePlate = "М678ХА23",
                Brand = "Yamaha",
                Category = CarCategory.Motorcycle,
                ClientId = 6,
                Client = clients[5]
            },
            new Car()
            {
                Id = 7,
                LicensePlate = "Н901СВ64",
                Brand = "Lada Vesta",
                Category = CarCategory.Passenger,
                ClientId = 7,
                Client = clients[6]
            },
            new Car()
            {
                Id = 8,
                LicensePlate = "Р234УЕ11",
                Brand = "Mercedes GLE",
                Category = CarCategory.Suv,
                ClientId = 8,
                Client = clients[7]
            },
            new Car()
            {
                Id = 9,
                LicensePlate = "Т567КМ33",
                Brand = "Ford Transit",
                Category = CarCategory.Truck,
                ClientId = 9,
                Client = clients[8]
            },
            new Car()
            {
                Id = 10,
                LicensePlate = "У890МН55",
                Brand = "Audi A6",
                Category = CarCategory.Passenger,
                ClientId = 10,
                Client = clients[9]
            },
            new Car()
            {
                Id = 11,
                LicensePlate = "Ф123АВ72",
                Brand = "Skoda Octavia",
                Category = CarCategory.Passenger,
                ClientId = 1,
                Client = clients[0]
            },
            new Car()
            {
                Id = 12,
                LicensePlate = "Х456ВС88",
                Brand = "Renault Duster",
                Category = CarCategory.Suv,
                ClientId = 2,
                Client = clients[1]
            },
        ];
    }

    /// <summary>
    /// Создаёт список заказов.
    /// </summary>
    public static List<Order> GetOrders(List<Client> clients, List<Service> services, List<Car> cars)
    {
        var now = DateTime.Now;
        var today = now.Date;

        return
        [
            new Order()
            {
                Id = 1,
                Client = clients[0],
                Service = services[0],
                Car = cars[0],
                StartTime = today.AddHours(9),
                BoxNumber = 1
            },
            new Order()
            {
                Id = 2,
                Client = clients[1],
                Service = services[1],
                Car = cars[1],
                StartTime = today.AddHours(10),
                BoxNumber = 2
            },
            new Order()
            {
                Id = 3,
                Client = clients[2],
                Service = services[5],
                Car = cars[2],
                StartTime = today.AddHours(11),
                BoxNumber = 3
            },
            new Order()
            {
                Id = 4,
                Client = clients[3],
                Service = services[3],
                Car = cars[3],
                StartTime = today.AddHours(12),
                BoxNumber = 4
            },
            new Order()
            {
                Id = 5,
                Client = clients[0],
                Service = services[2],
                Car = cars[0],
                StartTime = today.AddHours(13),
                BoxNumber = 1
            },
            new Order()
            {
                Id = 6,
                Client = clients[4],
                Service = services[0],
                Car = cars[4],
                StartTime = today.AddHours(14),
                BoxNumber = 2
            },
            new Order()
            {
                Id = 7,
                Client = clients[5],
                Service = services[7],
                Car = cars[5],
                StartTime = today.AddHours(15),
                BoxNumber = 5
            },
            new Order()
            {
                Id = 8,
                Client = clients[6],
                Service = services[1],
                Car = cars[6],
                StartTime = today.AddHours(16),
                BoxNumber = 1
            },
            new Order()
            {
                Id = 9,
                Client = clients[7],
                Service = services[8],
                Car = cars[7],
                StartTime = today.AddHours(17),
                BoxNumber = 3
            },
            new Order()
            {
                Id = 10,
                Client = clients[8],
                Service = services[3],
                Car = cars[8],
                StartTime = today.AddHours(18),
                BoxNumber = 4
            },
            new Order()
            {
                Id = 11,
                Client = clients[0],
                Service = services[4],
                Car = cars[0],
                StartTime = today.AddDays(1).AddHours(9),
                BoxNumber = 1
            },
            new Order()
            {
                Id = 12,
                Client = clients[9],
                Service = services[1],
                Car = cars[9],
                StartTime = today.AddDays(1).AddHours(10),
                BoxNumber = 2
            },
            new Order()
            {
                Id = 13,
                Client = clients[1],
                Service = services[0],
                Car = cars[1],
                StartTime = today.AddDays(-1).AddHours(11),
                BoxNumber = 1
            },
            new Order()
            {
                Id = 14,
                Client = clients[2],
                Service = services[9],
                Car = cars[2],
                StartTime = today.AddDays(-1).AddHours(12),
                BoxNumber = 3
            },
            new Order()
            {
                Id = 15,
                Client = clients[10],
                Service = services[2],
                Car = cars[0],
                StartTime = today.AddDays(-2).AddHours(13),
                BoxNumber = 1
            },
        ];
    }
}