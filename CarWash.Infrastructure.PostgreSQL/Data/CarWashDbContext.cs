using CarWash.Domain.Data;
using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.PostgreSQL.Data;

/// <summary>
/// Контекст базы данных автомойки.
/// </summary>
/// <param name="options">Настройки подключения.</param>
public class CarWashDbContext(DbContextOptions<CarWashDbContext> options) : DbContext(options)
{
    /// <summary>Клиенты.</summary>
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>Автомобили.</summary>
    public DbSet<Car> Cars => Set<Car>();

    /// <summary>Услуги.</summary>
    public DbSet<Service> Services => Set<Service>();

    /// <summary>Заказы.</summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>
    /// Настраивает модель базы данных.
    /// </summary>
    /// <param name="modelBuilder">Построитель модели.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("clients");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.ToTable("cars");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LicensePlate).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Brand).IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Client)
                .WithMany(c => c.Cars)
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("services");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StartTime).HasColumnType("timestamp without time zone");
            entity.HasOne(e => e.Client)
                .WithMany()
                .HasForeignKey("ClientId")
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Service)
                .WithMany()
                .HasForeignKey("ServiceId")
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Car)
                .WithMany()
                .HasForeignKey("CarId")
                .OnDelete(DeleteBehavior.Restrict);
        });

        Seed(modelBuilder);
    }

    /// <summary>
    /// Заполняет таблицы начальными данными.
    /// </summary>
    /// <param name="modelBuilder">Построитель модели.</param>
    private static void Seed(ModelBuilder modelBuilder)
    {
        var clients = CarWashDataSeeder.GetClients();
        var services = CarWashDataSeeder.GetServices();
        var cars = CarWashDataSeeder.GetCars(clients);
        var orders = CarWashDataSeeder.GetOrders(clients, services, cars);

        modelBuilder.Entity<Client>().HasData(clients);
        modelBuilder.Entity<Service>().HasData(services);
        modelBuilder.Entity<Car>().HasData(cars.Select(c => new
        {
            c.Id,
            c.LicensePlate,
            c.Brand,
            c.Category,
            c.ClientId
        }));
        modelBuilder.Entity<Order>().HasData(orders.Select(o => new
        {
            o.Id,
            ClientId = o.Client.Id,
            ServiceId = o.Service.Id,
            CarId = o.Car.Id,
            o.StartTime,
            o.BoxNumber
        }));
    }
}