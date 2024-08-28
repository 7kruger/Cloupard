using Cloupard.Application.Common.Exceptions;
using Cloupard.Application.Products.Commands.DeleteProduct;
using Cloupard.Application.Interfaces.Repositories;
using Cloupard.Tests.Common;

namespace Cloupard.Tests.Products.Commands;

[Collection("HandlerTestCollection")]
public class DeleteProductCommandHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteProductCommandHandlerTests(HandlerTestFixture fixture)
    {
        _unitOfWork = fixture.UnitOfWork;
    }

    [Fact]
    public async Task DeleteProductCommandHandler_Success()
    {
        var handler = new DeleteProductCommandHandler(_unitOfWork);

        await handler.Handle(new DeleteProductCommand(DbContextFactory.ProductIdForDelete), CancellationToken.None);

        Assert.Null(await _unitOfWork.Products.GetByIdAsync(DbContextFactory.ProductIdForDelete));
    }
    
    [Fact]
    public async Task DeleteNoteCommandHandler_FailOnWrongId()
    {
        var handler = new DeleteProductCommandHandler(_unitOfWork);
        
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(new DeleteProductCommand(Guid.NewGuid()), CancellationToken.None);
        });
    }
}