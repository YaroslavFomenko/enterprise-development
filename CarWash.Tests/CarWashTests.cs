namespace CarWash.Tests;

/// <summary>
/// Unit-тесты для аналитических запросов автомойки.
/// </summary>
public class CarWashTests(CarWashFixture fixture) : IClassFixture<CarWashFixture>
{
    /// <summary>
    /// Топ-5 клиентов по количеству посещений.
    /// </summary>
    [Fact]
    public void GetTop5ClientsByVisits_ShouldReturnOrderedByVisitCount()
    {
        // arrange
        var expectedTop5 = new[]
        {
            "Иванов Иван Иванович",
            "Петров Пётр Петрович",
            "Сидорова Анна Сергеевна",
            "Волков Сергей Андреевич",
            "Зайцева Татьяна Львовна"
        };

        // act
        var actualTop5 = fixture.Orders
            .GroupBy(o => o.Client)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.FullName)
            .Take(5)
            .Select(g => g.Key.FullName)
            .ToList();

        // assert
        Assert.Equal(expectedTop5, actualTop5);
    }

    /// <summary>
    /// Автомобили на мойке в указанный момент.
    /// </summary>
    [Fact]
    public void GetCarsInWashAtMoment_ShouldReturnOnlyActiveCars()
    {
        // arrange
        var moment = DateTime.Today.AddHours(9).AddMinutes(5);
        var expectedPlates = new[] { "А123ВС77" };

        // act
        var actualPlates = fixture.Orders
            .Where(o => o.StartTime <= moment
                        && moment < o.StartTime.AddMinutes(o.Service.DurationMinutes))
            .Select(o => o.Car.LicensePlate)
            .ToList();

        // assert
        Assert.Equal(expectedPlates, actualPlates);
    }

    /// <summary>
    /// Топ-5 наиболее популярных услуг.
    /// </summary>
    [Fact]
    public void GetTop5PopularServices_ShouldReturnOrderedByOrderCount()
    {
        // arrange
        var expectedTop5 = new[]
        {
            "Комплексная мойка",
            "Экспресс-мойка",
            "Мойка грузовика",
            "Химчистка салона",
            "Мойка внедорожника"
        };

        // act
        var actualTop5 = fixture.Orders
            .GroupBy(o => o.Service)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.Name)
            .Take(5)
            .Select(g => g.Key.Name)
            .ToList();

        // assert
        Assert.Equal(expectedTop5, actualTop5);
    }
    
    /// <summary>
    /// Время освобождения выбранного бокса, начиная с указанного момента.
    /// </summary>
    [Fact]
    public void GetBoxFreeTime_ShouldReturnEndTimeOfLastOrder()
    {
        // arrange
        const int boxNumber = 1;
        var fromMoment = DateTime.Today.AddHours(8);
        var expectedFreeTime = DateTime.Today.AddDays(1).AddHours(9).AddMinutes(90);

        // act
        var actualFreeTime = fixture.Orders
            .Where(o => o.BoxNumber == boxNumber && o.StartTime >= fromMoment)
            .OrderByDescending(o => o.StartTime)
            .Select(o => o.StartTime.AddMinutes(o.Service.DurationMinutes))
            .FirstOrDefault();

        // assert
        Assert.Equal(expectedFreeTime, actualFreeTime);
    }
    
    /// <summary>
    /// Суммарная выручка по каждой услуге.
    /// </summary>
    [Fact]
    public void GetTotalRevenueByService_ShouldReturnSumOfPrices()
    {
        // arrange
        var expectedRevenue = new Dictionary<string, decimal>
        {
            ["Экспресс-мойка"] = 900,
            ["Комплексная мойка"] = 1800,
            ["Химчистка салона"] = 5000,
            ["Мойка грузовика"] = 3000,
            ["Полировка кузова"] = 3000,
            ["Мойка внедорожника"] = 800,
            ["Мойка мотоцикла"] = 400,
            ["Нанесение воска"] = 1200,
            ["Уборка багажника"] = 500
        };

        // act
        var actualRevenue = fixture.Orders
            .GroupBy(o => o.Service.Name)
            .ToDictionary(g => g.Key, g => g.Sum(o => o.Service.Price));

        // assert
        Assert.Equal(expectedRevenue.Count, actualRevenue.Count);
        Assert.All(expectedRevenue, pair =>
            Assert.Equal(pair.Value, actualRevenue[pair.Key]));
    }
}