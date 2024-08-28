using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cloupard.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        var products = await _unitOfWork.Products.GetAll()
            .ProjectTo<ProductLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ProductListVm(products);
    }
}