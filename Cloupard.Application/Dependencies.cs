using System.Reflection;
using Cloupard.Application.Services;
using Cloupard.Application.Validations.FluentValidations;
using Cloupard.Domain.Dto.Products;
using Microsoft.Extensions.DependencyInjection;
using Cloupard.Domain.Interfaces.Services;
using FluentValidation;
using ProductValidator = Cloupard.Application.Validations.ProductValidator;

namespace Cloupard.Application;

public static class Dependencies
{
    public static void InjectApplicationDependencies(this IServiceCollection services)
    {
        InitServices(services);
        InitValidators(services);
        AddAutoMapper(services);
    }

    private static void InitServices(IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
    }

    private static void InitValidators(IServiceCollection services)
    {
        services.AddScoped<ProductValidator>();
        services.AddScoped<IValidator<CreateProductDto>, CreateProductDtoValidator>();
        services.AddScoped<IValidator<UpdateProductDto>, UpdateProductDtoValidator>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}