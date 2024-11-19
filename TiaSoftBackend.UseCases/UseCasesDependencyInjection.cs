using TiaSoftBackend.UseCases.Bills;
using TiaSoftBackend.UseCases.Mappers;
using TiaSoftBackend.UseCases.Tables;

namespace TiaSoftBackend.UseCases;

using Microsoft.Extensions.DependencyInjection;

public static class UseCasesDependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
        => services.AddVehicleUseCases();

    private static IServiceCollection AddVehicleUseCases(this IServiceCollection services)
        => services.AddScoped<TablesUseCases>()
            .AddScoped<GetTables>()
            .AddScoped<GetTable>()
            .AddScoped<UpdateTable>()
            .AddScoped<SendTableToCashier>()
            .AddScoped<GetTableStatuses>()
            .AddScoped<CreateTable>()
            .AddScoped<BillsUseCases>()
            .AddScoped<CreateBill>();

    public static IServiceCollection AddMappers(this IServiceCollection services)
        => services.AddAutoMapper(
            typeof(TablesProfile).Assembly, 
            typeof(AreasProfile).Assembly,
            typeof(UsersProfile).Assembly,
            typeof(BillsProfile).Assembly);
}