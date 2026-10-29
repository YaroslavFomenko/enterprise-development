using CarWash.Api.Dtos;

namespace CarWash.Api.Interfaces;

/// <summary>
///     Сервис для управления заказами автомойки.
/// </summary>
public interface IOrderService
{
    /// <summary>
    ///     Возвращает список всех заказов.
    /// </summary>
    /// <returns>Коллекция заказов.</returns>
    public IReadOnlyCollection<OrderDto> GetAll();

    /// <summary>
    ///     Возвращает заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>Заказ или null, если не найден.</returns>
    public OrderDto? GetById(int id);

    /// <summary>
    ///     Создает новый заказ.
    /// </summary>
    /// <param name="dto">Данные нового заказа.</param>
    /// <returns>Созданный заказ.</returns>
    public OrderDto Create(OrderCreateUpdateDto dto);

    /// <summary>
    ///     Обновляет существующий заказ.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <param name="dto">Новые данные заказа.</param>
    /// <returns>Обновленный заказ.</returns>
    public OrderDto Update(int id, OrderCreateUpdateDto dto);

    /// <summary>
    ///     Удаляет заказ.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>True, если удаление успешно, иначе False.</returns>
    public bool Delete(int id);
}