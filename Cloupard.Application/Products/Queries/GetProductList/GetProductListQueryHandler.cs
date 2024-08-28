using AutoMapper;
using Cloupard.Application.Interfaces.Repositories;
using Cloupard.Application.Specifications.Products;
using MediatR;

namespace Cloupard.Application.Products.Queries.GetProductList;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ProductListVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductListVm> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetProductListSpecification();
        var products = await _unitOfWork.Products.ListAsync(spec, cancellationToken);

        return new ProductListVm(_mapper.Map<IList<ProductLookupDto>>(products));
    }
}