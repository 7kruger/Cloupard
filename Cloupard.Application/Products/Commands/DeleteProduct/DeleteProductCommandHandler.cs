using Cloupard.Application.Common.Exceptions;
using Cloupard.Application.Interfaces.Repositories;
using Cloupard.Application.Specifications.Products;
using MediatR;

namespace Cloupard.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var spec = new GetProductSpecification(request.Id);
        var product = await _unitOfWork.Products.FirstOrDefaultAsync(spec, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(product), request.Id);
        }

        await _unitOfWork.Products.DeleteAsync(product, cancellationToken);
        
        return Unit.Value;
    }
}