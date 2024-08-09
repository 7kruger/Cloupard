using AutoMapper;
using Cloupard.Application.Products.Queries.GetProductList;
using Cloupard.Domain.Interfaces;
using Cloupard.Tests.Common;
using Shouldly;

namespace Cloupard.Tests.Products.Queries;

[Collection("HandlerTestCollection")]
public class GetProductListQueryHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductListQueryHandlerTests(HandlerTestFixture fixture)
    {
        _unitOfWork = fixture.UnitOfWork;
        _mapper = fixture.Mapper;
    }
    
    [Fact]
    public async Task GetProductListQueryHandler_Success()
    {
        var handler = new GetProductListQueryHandler(_unitOfWork, _mapper);

        var result = await handler.Handle(new GetProductListQuery(), CancellationToken.None);

        result.ShouldBeOfType<ProductListVm>();
        result.Products.Count.ShouldBe(5);
    }
}