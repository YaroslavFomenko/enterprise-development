using CarWash.Api.Dtos;

namespace CarWash.Api.Interfaces;

/// <summary>
///     Сервис для управления автомобилями.
/// </summary>
public interface ICarService
{
    /// <summary>
    ///     Возвращает список всех автомобилей.
    /// </summary>
    /// <returns>Коллекция автомобилей.</returns>
    public IReadOnlyCollection<CarDto> GetAll();

    /// <summary>
    ///     Возвращает автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Автомобиль или null, если не найден.</returns>
    public CarDto? GetById(int id);

    /// <summary>
    ///     Создает новый автомобиль.
    /// </summary>
    /// <param name="dto">Данные нового автомобиля.</param>
    /// <returns>Созданный автомобиль.</returns>
    public CarDto Create(CarCreateUpdateDto dto);

    /// <summary>
    ///     Обновляет существующий автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <param name="dto">Новые данные автомобиля.</param>
    /// <returns>Обновленный автомобиль.</returns>
    public CarDto Update(int id, CarCreateUpdateDto dto);

    /// <summary>
    ///     Удаляет автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>True, если удаление успешно, иначе False.</returns>
    public bool Delete(int id);
}