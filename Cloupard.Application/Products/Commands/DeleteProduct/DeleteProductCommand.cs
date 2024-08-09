using MediatR;

namespace Cloupard.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<Unit>;