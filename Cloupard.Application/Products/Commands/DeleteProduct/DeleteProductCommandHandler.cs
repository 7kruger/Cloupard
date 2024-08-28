using Cloupard.Application.Common.Exceptions;
using Cloupard.Application.Interfaces.Repositories;
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
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

        if (product == null)
        {
            throw new NotFoundException(nameof(product), request.Id);
        }

        await _unitOfWork.Products.DeleteAsync(product);
        
        return Unit.Value;
    }
}