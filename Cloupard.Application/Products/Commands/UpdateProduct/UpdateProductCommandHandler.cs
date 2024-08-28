using Cloupard.Application.Common.Exceptions;
using Cloupard.Domain.Entities;
using Cloupard.Application.Interfaces.Repositories;
using Cloupard.Application.Specifications.Products;
using MediatR;

namespace Cloupard.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var spec = new GetProductSpecification(request.Id);
        var product = await _unitOfWork.Products.FirstOrDefaultAsync(spec, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        product.Title = request.Title;
        product.Price = request.Price;
        product.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Products.UpdateAsync(product, cancellationToken);
        
        return Unit.Value;
    }
}