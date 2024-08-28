using Cloupard.Domain.Entities;
using Cloupard.Application.Interfaces.Repositories;
using MediatR;

namespace Cloupard.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Price = request.Price,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            IsDeleted = false,
            DeletedAt = null
        };

        await _unitOfWork.Products.AddAsync(product, cancellationToken);

        return product.Id;
    }
}