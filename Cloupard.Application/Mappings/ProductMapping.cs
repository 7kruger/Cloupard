using AutoMapper;
using Cloupard.Domain.Dto.Products;
using Cloupard.Domain.Entities;

namespace Cloupard.Application.Mappings;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<Product, ProductDto>()
            .ForCtorParam(nameof(ProductDto.Id), options => options.MapFrom(x => x.Id))
            .ForCtorParam(nameof(ProductDto.Title), options => options.MapFrom(x => x.Title))
            .ForCtorParam(nameof(ProductDto.Price), options => options.MapFrom(x => x.Price))
            .ReverseMap();
    }
}