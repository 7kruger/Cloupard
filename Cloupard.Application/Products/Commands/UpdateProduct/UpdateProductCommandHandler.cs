using Cloupard.Application.Common.Exceptions;
using Cloupard.Domain.Entities;
using Cloupard.Application.Interfaces.Repositories;
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
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        product.Title = request.Title;
        product.Price = request.Price;
        product.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Products.UpdateAsync(product);
        
        return Unit.Value;
    }
}