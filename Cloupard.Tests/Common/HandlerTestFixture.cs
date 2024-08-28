using AutoMapper;
using Cloupard.Application.Mappings;
using Cloupard.DAL.Repositories;
using Cloupard.Application.Interfaces.Repositories;

namespace Cloupard.Tests.Common;

public class HandlerTestFixture
{
    public readonly IUnitOfWork UnitOfWork;
    public readonly IMapper Mapper;

    public HandlerTestFixture()
    {
        UnitOfWork = new UnitOfWork(DbContextFactory.Create());
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductMapping>();
        });
        Mapper = configurationProvider.CreateMapper();
    }
}

[CollectionDefinition("HandlerTestCollection")]
public record HandlerTestsCollection : ICollectionFixture<HandlerTestFixture>;