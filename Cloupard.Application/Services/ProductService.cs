using System.Net;
using AutoMapper;
using Cloupard.Application.Validations.FluentValidations;
using Microsoft.EntityFrameworkCore;
using Cloupard.Domain.Dto.Products;
using Cloupard.Domain.Entities;
using Cloupard.Domain.Interfaces.Repositories;
using Cloupard.Domain.Interfaces.Services;
using Cloupard.Domain.Results;
using ProductValidator = Cloupard.Application.Validations.ProductValidator;

namespace Cloupard.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ProductValidator _productValidator;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ProductValidator productValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productValidator = productValidator;
    }

    public async Task<Result<List<ProductDto>>> GetAllAsync()
    {
        var products = await _unitOfWork.ProductsRepository.GetAll().ToListAsync();

        if (!products.Any())
        {
            return Result<List<ProductDto>>.Success([], (int)HttpStatusCode.NoContent, "Product list is empty");
        }
        
        return Result<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products));
    }

    public async Task<Result<ProductDto>> GetByIdAsync(long id)
    {
        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);

        var result = _productValidator.ValidateOnNull(product);
        if (!result.IsSuccess)
        {
            return Result<ProductDto>.Error(result.StatusCode, result.Message);
        }
        
        return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
    }

    public async Task<Result<ProductDto>> CreateAsync(CreateProductDto dto)
    {
        var validatorResult = new CreateProductDtoValidator().Validate(dto);

        if (!validatorResult.IsValid)
        {
            return Result<ProductDto>.Error((int)HttpStatusCode.UnprocessableContent, "Validation failed",
                validatorResult.Errors.Select(x => x.ErrorMessage));
        }
        
        var product = new Product
        {
            Title = dto.Title,
            Price = dto.Price
        };
        
        await _unitOfWork.ProductsRepository.CreateAsync(product);
        
        return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
    }

    public async Task<Result<ProductDto>> UpdateAsync(UpdateProductDto dto)
    {
        var validatorResult = new UpdateProductDtoValidator().Validate(dto);

        if (!validatorResult.IsValid)
        {
            return Result<ProductDto>.Error((int)HttpStatusCode.UnprocessableContent, "Validation failed",
                validatorResult.Errors.Select(x => x.ErrorMessage));
        }
        
        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(dto.Id);

        var result = _productValidator.ValidateOnNull(product);
        if (!result.IsSuccess)
        {
            return Result<ProductDto>.Error(result.StatusCode, result.Message);
        }
        
        product.Title = dto.Title;
        product.Price = dto.Price;

        await _unitOfWork.ProductsRepository.UpdateAsync(product);

        return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
    }

    public async Task<Result> DeleteAsync(long id)
    {
        var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
        
        var result = _productValidator.ValidateOnNull(product);
        if (!result.IsSuccess)
        {
            return Result.Error(result.StatusCode, result.Message);
        }

        await _unitOfWork.ProductsRepository.DeleteAsync(product);

        return Result.Success();
    }
}