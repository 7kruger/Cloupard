using MediatR;

namespace Cloupard.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Title, double Price) : IRequest<Unit>;