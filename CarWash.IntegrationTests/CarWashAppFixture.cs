using Aspire.Hosting;
using Aspire.Hosting.Testing;
using Xunit;

namespace CarWash.IntegrationTests;

/// <summary>
/// Запускает API с отдельной PostgreSQL через Aspire.
/// </summary>
public class CarWashAppFixture : IAsyncLifetime
{
    private DistributedApplication? _app;

    /// <summary>
    /// Клиент API.
    /// </summary>
    public HttpClient Client { get; private set; } = null!;

    /// <summary>
    /// Строка подключения к тестовой БД.
    /// </summary>
    public string ConnectionString { get; private set; } = null!;

    /// <inheritdoc />
    public async ValueTask InitializeAsync()
    {
        using var timeout = new CancellationTokenSource(TimeSpan.FromMinutes(3));
        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.CarWash_AppHost>(["UseVolumes=false"], timeout.Token);

        builder.Configuration["AppHost:FilePath"] = string.Empty;
        _app = await builder.BuildAsync(timeout.Token);
        await _app.StartAsync(timeout.Token);
        
        await _app.ResourceNotifications.WaitForResourceHealthyAsync("api", timeout.Token);
        
        Client = _app.CreateHttpClient("api", "http");

        ConnectionString = await _app.GetConnectionStringAsync("carwash", timeout.Token)
                           ?? throw new InvalidOperationException("Нет строки подключения к тестовой БД.");
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        Client?.Dispose();
        if (_app is not null) await _app.DisposeAsync();
    }
}