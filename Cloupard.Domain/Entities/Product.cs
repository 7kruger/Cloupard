using Cloupard.Domain.Interfaces;

namespace Cloupard.Domain.Entities;

public class Product : IEntityId<long>, IAuditable, ISoftDeletable
{
    public long Id { get; set; }
    public string Title { get; set; }
    public double Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}