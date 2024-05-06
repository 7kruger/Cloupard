using Cloupard.Domain.Dto.Products;
using Cloupard.Domain.Results;

namespace Cloupard.Domain.Interfaces.Services;

public interface IProductService
{
    Task<Result<List<ProductDto>>> GetAllAsync();
    Task<Result<ProductDto>> GetByIdAsync(long id);
    Task<Result<ProductDto>> CreateAsync(CreateProductDto dto);
    Task<Result<ProductDto>> UpdateAsync(UpdateProductDto dto);
    Task<Result> DeleteAsync(long id);
}