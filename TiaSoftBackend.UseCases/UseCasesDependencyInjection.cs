using TiaSoftBackend.UseCases.Areas;
using TiaSoftBackend.UseCases.Bills;
using TiaSoftBackend.UseCases.Mappers;
using TiaSoftBackend.UseCases.Menus;
using TiaSoftBackend.UseCases.Tables;

namespace TiaSoftBackend.UseCases;

using Microsoft.Extensions.DependencyInjection;

public static class UseCasesDependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
        => services.AddTableUseCases()
            .AddBillUseCases()
            .AddAreaUseCases()
            .AddMenuUseCases();
    
    private static IServiceCollection AddTableUseCases(this IServiceCollection services)
        => services.AddScoped<TablesUseCases>()
            .AddScoped<GetTables>()
            .AddScoped<GetTable>()
            .AddScoped<UpdateTable>()
            .AddScoped<SendTableToCashier>()
            .AddScoped<GetTableStatuses>()
            .AddScoped<CreateTable>();
    
    private static IServiceCollection AddBillUseCases(this IServiceCollection services)
        => services.AddScoped<BillsUseCases>()
            .AddScoped<CreateBill>();

    private static IServiceCollection AddAreaUseCases(this IServiceCollection services)
        => services.AddScoped<AreasUseCases>()
            .AddScoped<GetAreas>()
            .AddScoped<CreateArea>()
            .AddScoped<CreateArea>()
            .AddScoped<UpdateArea>();
    
    private static IServiceCollection AddMenuUseCases(this IServiceCollection services)
        => services.AddScoped<MenuUseCases>()
            .AddScoped<GetMenus>()
            .AddScoped<CreateMenu>()
            .AddScoped<UpdateMenu>()
            .AddScoped<UploadMenuImage>();

    public static IServiceCollection AddMappers(this IServiceCollection services)
        => services.AddAutoMapper(
            typeof(TablesProfile).Assembly,
            typeof(AreasProfile).Assembly,
            typeof(UsersProfile).Assembly,
            typeof(BillsProfile).Assembly,
            typeof(MenusProfile).Assembly);
}