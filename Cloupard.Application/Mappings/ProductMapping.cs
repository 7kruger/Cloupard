using AutoMapper;
using Cloupard.Application.Products.Queries.GetProduct;
using Cloupard.Application.Products.Queries.GetProductList;
using Cloupard.Domain.Entities;

namespace Cloupard.Application.Mappings;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<Product, GetProductVm>();
        CreateMap<Product, ProductLookupDto>();
    }
}