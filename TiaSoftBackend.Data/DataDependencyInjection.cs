using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TiaSoftBackend.Data.Repositories;

namespace TiaSoftBackend.Data;

public static class DataDependencyInjection
{
    public static IServiceCollection AddData(this IServiceCollection services)
        => services.AddScoped<ICategoriesRepository, CategoriesRepository>()
            .AddScoped<ITablesRepository, TablesRepository>()
            .AddScoped<ITableStatusesRepository, TableStatusesRepository>();
}