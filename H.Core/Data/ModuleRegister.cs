using H.Core.Data.EFCore.DbContextFactory;
using H.Core.Data.EFCore.Repository;
using H.Core.Data.EFCore.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace H.Core.Data;

public static class ModuleRegister
{
    public static void RegisterCoreData(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddTransient<IDbContextFactory, DbContextFactory>();
    }
}