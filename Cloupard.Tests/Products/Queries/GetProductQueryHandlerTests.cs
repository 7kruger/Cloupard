using AutoMapper;
using Cloupard.Application.Common.Exceptions;
using Cloupard.Application.Interfaces.Repositories;
using Cloupard.Application.Products.Queries.GetProduct;
using Cloupard.Tests.Common;
using Shouldly;

namespace Cloupard.Tests.Products.Queries;

[Collection("HandlerTestCollection")]
public class GetProductQueryHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetProductQueryHandlerTests(HandlerTestFixture fixture)
    {
        _unitOfWork = fixture.UnitOfWork;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task GetProductQueryHandler_Success()
    {
        var handler = new GetProductQueryHandler(_unitOfWork, _mapper);

        var result = await handler.Handle(new GetProductQuery(DbContextFactory.ProductIdToGet), CancellationToken.None);

        result.ShouldBeOfType<GetProductVm>();
        result.Title.ShouldBe("Product 2");
        result.Price.ShouldBe(666);
    }

    [Fact]
    public async Task GetProductQueryHandler_FailOnWrongId()
    {
        var handler = new GetProductQueryHandler(_unitOfWork, _mapper);
        
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(new GetProductQuery(Guid.NewGuid()), CancellationToken.None);
        });
    }
}