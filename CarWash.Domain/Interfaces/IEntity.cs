namespace CarWash.Domain.Interfaces;

/// <summary>
/// Интерфейс для сущностей, имеющих идентификатор.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Идентификатор сущности.
    /// </summary>
    public int Id { get; set; }
}