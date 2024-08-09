using Cloupard.Domain.Interfaces;

namespace Cloupard.Domain.Entities;

public class Product : IAuditable, ISoftDeletable
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public double Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
}