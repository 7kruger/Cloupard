using Ardalis.Specification;
using Cloupard.Domain.Entities;

namespace Cloupard.Application.Specifications.Products;

public sealed class GetProductSpecification : Specification<Product>
{
    public GetProductSpecification(Guid id)
    {
        Query.Where(product => product.Id == id);
    }
}