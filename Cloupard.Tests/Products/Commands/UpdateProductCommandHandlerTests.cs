using Cloupard.Application.Common.Exceptions;
using Cloupard.Application.Products.Commands.UpdateProduct;
using Cloupard.Application.Interfaces.Repositories;
using Cloupard.Tests.Common;

namespace Cloupard.Tests.Products.Commands;

[Collection("HandlerTestCollection")]
public class UpdateProductCommandHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateProductCommandHandlerTests(HandlerTestFixture fixture)
    {
        _unitOfWork = fixture.UnitOfWork;
    }

    [Fact]
    public async Task UpdateProductCommandHandler_Success()
    {
        var handler = new UpdateProductCommandHandler(_unitOfWork);

        await handler.Handle(
            new UpdateProductCommand(DbContextFactory.ProductIdForUpdate, "Product 3 (edited)", 1),
            CancellationToken.None);

        Assert.NotNull(await _unitOfWork.Products.GetByIdAsync(DbContextFactory.ProductIdForUpdate));
    }
    
    [Fact]
    public async Task UpdateNoteCommandHandler_FailOnWrongId()
    {
        var handler = new UpdateProductCommandHandler(_unitOfWork);

        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(
                new UpdateProductCommand(Guid.NewGuid(), "Product 3 (edited)", 1),
                CancellationToken.None);
        });
    }
}