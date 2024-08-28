using AutoMapper;
using Cloupard.Application.Common.Exceptions;
using Cloupard.Domain.Entities;
using Cloupard.Application.Interfaces.Repositories;
using Cloupard.Application.Specifications.Products;
using MediatR;

namespace Cloupard.Application.Products.Queries.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetProductVm> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetProductSpecification(request.Id);
        var product = await _unitOfWork.Products.FirstOrDefaultAsync(spec, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        return _mapper.Map<GetProductVm>(product);
    }
}