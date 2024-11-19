using TiaSoftBackend.UseCases.Areas;
using TiaSoftBackend.UseCases.Bills;
using TiaSoftBackend.UseCases.Categories;
using TiaSoftBackend.UseCases.Mappers;
using TiaSoftBackend.UseCases.Menus;
using TiaSoftBackend.UseCases.Orders;
using TiaSoftBackend.UseCases.Tables;

namespace TiaSoftBackend.UseCases;

using Microsoft.Extensions.DependencyInjection;

public static class UseCasesDependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
        => services.AddTableUseCases()
            .AddBillUseCases()
            .AddAreaUseCases()
            .AddMenuUseCases()
            .AddCategoryUseCases()
            .AddOrderUseCases();
    
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
            .AddScoped<CreateBill>()
            .AddScoped<UpdateBill>();

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
    
    private static IServiceCollection AddCategoryUseCases(this IServiceCollection services)
        => services.AddScoped<CategoriesUseCases>()
            .AddScoped<GetCategories>()
            .AddScoped<CreateCategory>()
            .AddScoped<UpdateCategory>();

    private static IServiceCollection AddOrderUseCases(this IServiceCollection services)
        => services.AddScoped<OrdersUseCases>()
            .AddScoped<CreateOrder>()
            .AddScoped<GetOrders>();

    public static IServiceCollection AddMappers(this IServiceCollection services)
        => services.AddAutoMapper(
            typeof(TablesProfile).Assembly,
            typeof(AreasProfile).Assembly,
            typeof(UsersProfile).Assembly,
            typeof(BillsProfile).Assembly,
            typeof(MenusProfile).Assembly,
            typeof(CategoriesProfile).Assembly,
            typeof(OrdersProfile).Assembly,
            typeof(RolesProfile).Assembly);
}