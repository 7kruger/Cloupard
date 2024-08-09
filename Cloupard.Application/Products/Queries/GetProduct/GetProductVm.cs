namespace Cloupard.Application.Products.Queries.GetProduct;

public record GetProductVm(Guid Id, string Title, double Price, DateTime CreatedAt, DateTime UpdatedAt);