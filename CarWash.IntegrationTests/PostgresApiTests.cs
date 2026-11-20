using System.Net;
using System.Net.Http.Json;
using CarWash.Application.Contracts.Dtos;

namespace CarWash.IntegrationTests;

/// <summary>
/// Интеграционные тесты API с PostgreSQL.
/// </summary>
public class PostgresApiTests(CarWashAppFixture fixture) : IClassFixture<CarWashAppFixture>
{
    /// <summary>
    /// Должен возвращать список клиентов из базы.
    /// </summary>
    [Fact]
    public async Task GetClients_WhenDatabaseHasData_ReturnsSuccessAndNonEmptyList()
    {
        // act
        var response = await fixture.Client.GetAsync("/api/clients", TestContext.Current.CancellationToken);

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var clients = await response.Content.ReadFromJsonAsync<List<ClientDto>>(TestContext.Current.CancellationToken);
        Assert.NotNull(clients);
        Assert.NotEmpty(clients);
    }

    /// <summary>
    /// Должен создавать и удалять клиента в базе.
    /// </summary>
    [Fact]
    public async Task  CreateAndDeleteClient_WhenValidData_ReturnsCreatedAndNoContent()
    {
        // arrange
        var dto = new ClientCreateUpdateDto("Тестовый Клиент", "+7 (999) 000-00-00");

        // act
        var createResponse = await fixture.Client.PostAsJsonAsync("/api/clients", dto, TestContext.Current.CancellationToken);
        
        // assert
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var created = await createResponse.Content.ReadFromJsonAsync<ClientDto>(TestContext.Current.CancellationToken);
        Assert.NotNull(created);

        // act
        var deleteResponse = await fixture.Client.DeleteAsync($"/api/clients/{created.Id}", TestContext.Current.CancellationToken);
        
        // assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }
}