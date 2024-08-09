using Cloupard.DAL.Data;
using Cloupard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cloupard.Tests.Common;

public static class DbContextFactory
{
    public static readonly Guid ProductIdToGet = Guid.NewGuid();
    public static readonly Guid ProductIdForDelete = Guid.NewGuid();
    public static readonly Guid ProductIdForUpdate = Guid.NewGuid();
    
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);

        const string userId = "77d5b1fc-c24d-47dc-87a5-ff577aca6b78";
        
        context.Products.AddRange([
            new Product
            {
                Id = Guid.NewGuid(),
                Title = "Product 1",
                Price = 21000,
                CreatedAt = DateTime.Today,
                CreatedBy = Guid.Parse(userId),
                UpdatedAt = null,
                UpdatedBy = null,
                DeletedAt = null,
                DeletedBy = null,
                IsDeleted = false
            },
            new Product
            {
                Id = ProductIdToGet,
                Title = "Product 2",
                Price = 666,
                CreatedAt = DateTime.Today,
                CreatedBy = Guid.Parse(userId),
                UpdatedAt = null,
                UpdatedBy = null,
                DeletedAt = null,
                DeletedBy = null,
                IsDeleted = false
            },
            new Product
            {
                Id = ProductIdForUpdate,
                Title = "Product 3",
                Price = 100,
                CreatedAt = DateTime.Today,
                CreatedBy = Guid.Parse(userId),
                UpdatedAt = null,
                UpdatedBy = null,
                DeletedAt = null,
                DeletedBy = null,
                IsDeleted = false
            },
            new Product
            {
                Id = ProductIdForDelete,
                Title = "Product 4",
                Price = 450000,
                CreatedAt = DateTime.Today,
                CreatedBy = Guid.Parse(userId),
                UpdatedAt = null,
                UpdatedBy = null,
                DeletedAt = null,
                DeletedBy = null,
                IsDeleted = false
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Title = "Product 5",
                Price = 1200000,
                CreatedAt = DateTime.Today,
                CreatedBy = Guid.Parse(userId),
                UpdatedAt = null,
                UpdatedBy = null,
                DeletedAt = null,
                DeletedBy = null,
                IsDeleted = false
            },
        ]);
        context.SaveChanges();
        return context;
    }

    public static void Destroy(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}