using Cloupard.Application.Products.Commands.CreateProduct;
using Cloupard.Application.Interfaces.Repositories;
using Cloupard.Tests.Common;
using Shouldly;

namespace Cloupard.Tests.Products.Commands;

[Collection("HandlerTestCollection")]
public class CreateProductCommandHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateProductCommandHandlerTests(HandlerTestFixture fixture)
    {
        _unitOfWork = fixture.UnitOfWork;
    }

    [Fact]
    public async Task CreateProductCommandHandler_Success()
    {
        var handler = new CreateProductCommandHandler(_unitOfWork);

        var productId = await handler.Handle(new CreateProductCommand("Product 6", 50000), CancellationToken.None);
        
        productId.ShouldNotBe(Guid.Empty);
    }
}