# CarWash — Система управления автомойкой

## Лабораторная работа №2: Сервер (ASP.NET Core Web API)

Реализовано REST API для управления автомойкой с использованием ASP.NET Core (.NET 10). Данные хранятся в памяти (in-memory) и инициализируются тестовым сидером из доменного проекта.

### Технологии

- .NET 10
- ASP.NET Core Web API
- Swagger (Swashbuckle.AspNetCore)
- xUnit v3 (тесты доменной модели)
- GitHub Actions (CI)

### Архитектура

Проект разделен на слои:
- CarWash.Domain — доменные сущности (Client, Car, Service, Order), перечисления и сидер данных.
- CarWash.Api — контроллеры, DTO, сервисы и настройка Swagger.
- CarWash.Tests — юнит-тесты аналитических запросов.

### Основные возможности

#### CRUD-операции
Для каждой сущности реализованы полные CRUD-операции:
- Услуги (/api/services) — создание, чтение, обновление, удаление.
- Клиенты (/api/clients) — управление клиентами.
- Автомобили (/api/cars) — управление автомобилями клиентов.
- Заказы (/api/orders) — оформление заказов на обслуживание.

#### Аналитические эндпоинты
- GET /api/analytics/top-clients — Топ-5 клиентов по количеству посещений.
- GET /api/analytics/cars-in-wash — Автомобили, находящиеся на мойке в указанный момент.
- GET /api/analytics/top-services — Топ-5 наиболее популярных услуг.
- GET /api/analytics/box-release-time — Время освобождения выбранного бокса.
- GET /api/analytics/revenue-by-service — Суммарная выручка по каждой услуге.

### Валидация и обработка ошибок

- Все DTO снабжены атрибутами валидации (Required, Range, StringLength, Phone).
- Контроллеры корректно возвращают HTTP-статусы (200, 201, 204, 400, 404, 500).
- Ошибки логируются через ILogger.

### Документация

Swagger UI доступен по адресу:
http://localhost:<порт>/swagger

### Запуск

dotnet run --project CarWash.Api

### Тестирование

dotnet test CarWash.Tests