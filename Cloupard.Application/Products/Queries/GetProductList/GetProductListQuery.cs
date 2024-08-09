using Cloupard.Application.Products.Queries.GetProduct;
using MediatR;

namespace Cloupard.Application.Products.Queries.GetProductList;

public record GetProductListQuery : IRequest<ProductListVm>;