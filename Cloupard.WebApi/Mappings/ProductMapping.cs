using AutoMapper;
using Cloupard.Application.Products.Commands.CreateProduct;
using Cloupard.Application.Products.Commands.UpdateProduct;
using Cloupard.WebApi.Models;

namespace Cloupard.WebApi.Mappings;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<CreateProductDto, CreateProductCommand>();
        CreateMap<UpdateProductDto, UpdateProductCommand>();
    }
}