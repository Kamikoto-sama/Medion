using Microsoft.Extensions.DependencyInjection.Medion.Common;

namespace Microsoft.Extensions.DependencyInjection.Medion.Upsert;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UpsertSingleton<TService, TImpl>(this IServiceCollection services)
        where TService : class
        where TImpl : class, TService
    {
        return services.Upsert(ServiceLifetime.Singleton, typeof(TService), implementationType: typeof(TImpl));
    }

    public static IServiceCollection UpsertSingleton<TService, TImpl>(
        this IServiceCollection services,
        object serviceKey)
        where TService : class
        where TImpl : class, TService
    {
        return services.Upsert(ServiceLifetime.Singleton, typeof(TService), serviceKey, typeof(TImpl));
    }

    public static IServiceCollection UpsertSingleton<TService>(
        this IServiceCollection services,
        TService implementationInstance)
        where TService : class
    {
        return services.Upsert(
            ServiceLifetime.Singleton,
            typeof(TService),
            implementationInstance: implementationInstance
        );
    }

    public static IServiceCollection UpsertSingleton<TService>(
        this IServiceCollection services,
        TService implementationInstance,
        object serviceKey)
        where TService : class
    {
        return services.Upsert(
            ServiceLifetime.Singleton,
            typeof(TService),
            serviceKey,
            implementationInstance: implementationInstance
        );
    }

    public static IServiceCollection Upsert(
        this IServiceCollection services,
        ServiceLifetime lifetime,
        Type serviceType,
        object? serviceKey = null,
        Type? implementationType = null,
        object? implementationInstance = null,
        Func<IServiceProvider, object>? implementationFactory = null,
        Func<IServiceProvider, object?, object>? keyedImplementationFactory = null)
    {
        var upsertIndices = FindIndices(services, serviceType, serviceKey);
        var serviceUpsert = ServiceDescriptorFactory.Create(
            lifetime,
            serviceType,
            serviceKey,
            implementationType,
            implementationInstance,
            implementationFactory,
            keyedImplementationFactory
        );

        if (upsertIndices.Length > 0)
            foreach (var upsertIndex in upsertIndices)
                services[upsertIndex] = serviceUpsert;
        else
            services.Add(serviceUpsert);

        return services;
    }

    private static int[] FindIndices(
        IServiceCollection services,
        Type serviceType,
        object? serviceKey = null)
    {
        return services
            .Select((s, i) => (service: s, Index: i))
            .Where(x => x.service.ServiceType == serviceType && x.service.ServiceKey == serviceKey)
            .Select(x => x.Index)
            .ToArray();
    }
}