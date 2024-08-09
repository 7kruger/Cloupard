using MediatR;

namespace Cloupard.Application.Products.Queries.GetProduct;

public record GetProductQuery(Guid Id) : IRequest<GetProductVm>;