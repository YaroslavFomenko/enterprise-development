# CarWash — Система управления автомойкой

## Лабораторная работа №3: ORM (PostgreSQL + Aspire)

В рамках третьей лабораторной работы было реализовано хранение данных в реляционной базе данных **PostgreSQL** с использованием **Entity Framework Core**. Настроена оркестрация приложения через **.NET Aspire**. Созданы **миграции** для формирования схемы базы данных и первичного заполнения.

### Технологии

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL (Npgsql)
- .NET Aspire
- xUnit v3
- GitHub Actions

### Архитектура решения

Решение разделено на проекты:
- **CarWash.Domain** — доменные сущности (`Client`, `Car`, `Service`, `Order`) и генератор начальных данных (`CarWashDataSeeder`).
- **CarWash.Application.Contracts** — DTO, интерфейсы сервисов и репозиториев.
- **CarWash.Application** — бизнес-логика (`ClientService`, `CarService`, `ServiceService`, `OrderService`, `AnalyticsService`).
- **CarWash.Infrastructure.PostgreSQL** — реализация ORM: `CarWashDbContext`, конфигурация связей, репозитории на EF Core, миграции.
- **CarWash.Infrastructure.InMemory** — реализация репозиториев в памяти (используется для юнит-тестов).
- **CarWash.ServiceDefaults** — конфигурация телеметрии и health checks для Aspire.
- **CarWash.Api** — REST-контроллеры и `Program.cs`.
- **CarWash.AppHost** — проект оркестратора Aspire (запуск контейнера PostgreSQL и API).
- **CarWash.Tests** — юнит-тесты.
- **CarWash.IntegrationTests** — интеграционные тесты с Aspire.

### Реализация ORM (EF Core)

- **Контекст БД:** создан `CarWashDbContext`, содержащий `DbSet` для всех сущностей.
- **Маппинг:** для всех таблиц заданы имена (например, `clients`, `orders`) и ограничения (например, `HasMaxLength`). Настроено преобразование `DateTime` в тип PostgreSQL `timestamp without time zone` во избежание ошибок драйвера Npgsql.
- **Связи:** настроены отношения «один-ко-многим» (Клиент — Автомобили, Заказы; Услуги; и т.д.) с ограничением `DeleteBehavior.Restrict`.
- **Загрузка данных:** в репозитории заказов реализована явная загрузка связанных сущностей (Eager Loading) с помощью `.Include()`, чтобы избежать `NullReferenceException` при обращении к `Client`, `Car` и `Service`.
- **Репозитории:** базовый класс `PostgreSqlRepository<T>` реализует CRUD через `DbSet`. Для него созданы наследники (`ClientRepository`, `OrderRepository` и др.).

### Миграции и заполнение

- Схема базы данных и начальные данные (сиды) генерируются автоматически через EF Core Migrations.
- Первичное заполнение (12 клиентов, 12 автомобилей, 10 услуг, 15 заказов) задано в `OnModelCreating` с помощью `HasData`.
- Применение миграций (`MigrateAsync`) выполняется автоматически при старте приложения.

### Тесты

**Запуск всех тестов:**
```bash
dotnet test CarWash.sln
```

**Интеграционные тесты (с Aspire + PostgreSQL):**
```bash
dotnet test CarWash.IntegrationTests
```

### Аналитические запросы

- `GET /api/analytics/top-clients` — Топ-5 клиентов по посещениям.
- `GET /api/analytics/cars-in-wash` — Автомобили на мойке в указанный момент.
- `GET /api/analytics/top-services` — Топ-5 популярных услуг.
- `GET /api/analytics/box-release-time` — Время освобождения бокса.
- `GET /api/analytics/revenue-by-service` — Выручка по услугам.