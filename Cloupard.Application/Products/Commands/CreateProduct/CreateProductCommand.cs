using MediatR;

namespace Cloupard.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(string Title, double Price) : IRequest<Guid>;