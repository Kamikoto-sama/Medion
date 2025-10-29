namespace Microsoft.Extensions.DependencyInjection.Medion.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScoped<TService, TImpl>(
        this IServiceCollection services,
        TImpl implementationInstance)
        where TService : class
        where TImpl : class, TService
    {
        return services.AddScoped<TService>(_ => implementationInstance);
    }

    public static IServiceCollection AddKeyedScoped<TService, TImpl>(
        this IServiceCollection services,
        object serviceKey,
        TImpl implementationInstance)
        where TService : class
        where TImpl : class, TService
    {
        return services.AddKeyedScoped<TService>(serviceKey, (_, _) => implementationInstance);
    }

    public static IServiceCollection AddTransient<TService, TImpl>(
        this IServiceCollection services,
        TImpl implementationInstance)
        where TService : class
        where TImpl : class, TService
    {
        return services.AddTransient<TService>(_ => implementationInstance);
    }

    public static IServiceCollection AddTransient<TService, TImpl>(
        this IServiceCollection services,
        object serviceKey,
        TImpl implementationInstance)
        where TService : class
        where TImpl : class, TService
    {
        return services.AddKeyedTransient<TService>(serviceKey, (_, _) => implementationInstance);
    }
}