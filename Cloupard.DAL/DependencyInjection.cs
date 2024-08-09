using Cloupard.DAL.Data;
using Cloupard.DAL.Interceptors;
using Cloupard.DAL.Repositories;
using Cloupard.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cloupard.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDal(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddUnitOfWork(services);
        return services;
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("postgres");
        
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString);
            options.AddInterceptors(serviceProvider.GetServices<IInterceptor>().ToArray());
        });
        
        services.AddSingleton<IInterceptor, AuditInterceptor>();
        services.AddSingleton<IInterceptor, SoftDeleteInterceptor>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    }

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}