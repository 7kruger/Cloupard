using Cloupard.DAL.Interceptors;
using Cloupard.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cloupard.Domain.Interfaces;
using Cloupard.Domain.Interfaces.Repositories;

namespace Cloupard.DAL;

public static class Dependencies
{
    public static void InjectDalDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        InitDbContext(services, configuration);
        InitRepositories(services);
        InitUnitOfWork(services);
    }

    private static void InitDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("postgres_local");

        services.AddSingleton<AuditInterceptor>();
        services.AddSingleton<SoftDeleteInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
    }

    private static void InitRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    }

    private static void InitUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}