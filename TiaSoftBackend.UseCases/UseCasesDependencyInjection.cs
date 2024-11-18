namespace TiaSoftBackend.UseCases;

using Microsoft.Extensions.DependencyInjection;

public static class UseCasesDependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
        => services.AddVehicleUseCases();
    
    private static IServiceCollection AddVehicleUseCases(this IServiceCollection services)
        => services;
}