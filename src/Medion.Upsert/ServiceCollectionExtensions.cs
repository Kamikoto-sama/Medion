using Microsoft.Extensions.DependencyInjection.Medion.Core;

namespace Microsoft.Extensions.DependencyInjection.Medion.Upsert;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Adds or replaces all occurrences of <typeparamref name="TService" />
    ///     in provided service collection with Singleton lifetime
    /// </summary>
    /// <typeparam name="TService">Service type to replace</typeparam>
    /// <typeparam name="TImpl">Replacing type</typeparam>
    /// <param name="services">Service collection</param>
    /// <returns>The modified service collection</returns>
    public static IServiceCollection UpsertSingleton<TService, TImpl>(this IServiceCollection services)
        where TService : class
        where TImpl : class, TService
    {
        return services.Upsert(ServiceLifetime.Singleton, typeof(TService), implementationType: typeof(TImpl));
    }

    /// <summary>
    ///     Adds or replaces all occurrences of <typeparamref name="TService" />
    ///     in provided service collection with Singleton lifetime and specified key
    /// </summary>
    /// <typeparam name="TService">Service type to replace</typeparam>
    /// <typeparam name="TImpl">Replacing type</typeparam>
    /// <param name="services">Service collection</param>
    /// <param name="serviceKey">Service key</param>
    /// <returns>The modified service collection</returns>
    public static IServiceCollection UpsertKeyedSingleton<TService, TImpl>(
        this IServiceCollection services,
        object serviceKey)
        where TService : class
        where TImpl : class, TService
    {
        return services.Upsert(ServiceLifetime.Singleton, typeof(TService), serviceKey, typeof(TImpl));
    }

    /// <summary>
    ///     Adds or replaces all occurrences of <typeparamref name="TService" />
    ///     in provided service collection with Singleton lifetime using implementation instance
    /// </summary>
    /// <typeparam name="TService">Service type to replace</typeparam>
    /// <param name="services">Service collection</param>
    /// <param name="implementationInstance">Implementation instance</param>
    /// <returns>The modified service collection</returns>
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

    /// <summary>
    ///     Adds or replaces all occurrences of <typeparamref name="TService" />
    ///     in provided service collection with Singleton lifetime using implementation instance and specified key
    /// </summary>
    /// <typeparam name="TService">Service type to replace</typeparam>
    /// <param name="services">Service collection</param>
    /// <param name="implementationInstance">Implementation instance</param>
    /// <param name="serviceKey">Service key</param>
    /// <returns>The modified service collection</returns>
    public static IServiceCollection UpsertKeyedSingleton<TService>(
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

    /// <summary>
    ///     Adds or replaces all occurrences of <typeparamref name="TService" />
    ///     in provided service collection with Singleton lifetime using implementation factory
    /// </summary>
    /// <typeparam name="TService">Service type to replace</typeparam>
    /// <param name="services">Service collection</param>
    /// <param name="implementationFactory">Implementation factory</param>
    /// <returns>The modified service collection</returns>
    public static IServiceCollection UpsertSingleton<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        return services.Upsert(
            ServiceLifetime.Singleton,
            typeof(TService),
            implementationFactory: implementationFactory
        );
    }

    /// <summary>
    ///     Adds or replaces all occurrences of <typeparamref name="TService" />
    ///     in provided service collection with Singleton lifetime using implementation factory and specified key
    /// </summary>
    /// <typeparam name="TService">Service type to replace</typeparam>
    /// <param name="services">Service collection</param>
    /// <param name="keyedImplementationFactory">Keyed implementation factory</param>
    /// <param name="serviceKey">Service key</param>
    /// <returns>The modified service collection</returns>
    public static IServiceCollection UpsertKeyedSingleton<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, object?, TService> keyedImplementationFactory,
        object serviceKey)
        where TService : class
    {
        return services.Upsert(
            ServiceLifetime.Singleton,
            typeof(TService),
            serviceKey,
            keyedImplementationFactory: keyedImplementationFactory
        );
    }

    /// <summary>
    ///     Adds or replaces all occurrences of <typeparamref name="TService" />
    ///     in provided service collection with Scoped lifetime
    /// </summary>
    /// <typeparam name="TService">Service type to replace</typeparam>
    /// <typeparam name="TImpl">Replacing type</typeparam>
    /// <param name="services">Service collection</param>
    /// <returns>The modified service collection</returns>
    public static IServiceCollection UpsertScoped<TService, TImpl>(this IServiceCollection services)
        where TService : class
        where TImpl : class, TService
    {
        return services.Upsert(ServiceLifetime.Scoped, typeof(TService), implementationType: typeof(TImpl));
    }

    /// <summary>
    ///     Adds or replaces all occurrences of <typeparamref name="TService" />
    ///     in provided service collection with Scoped lifetime and specified key
    /// </summary>
    /// <typeparam name="TService">Service type to replace</typeparam>
    /// <typeparam name="TImpl">Replacing type</typeparam>
    /// <param name="services">Service collection</param>
    /// <param name="serviceKey">Service key</param>
    /// <returns>The modified service collection</returns>
    public static IServiceCollection UpsertKeyedScoped<TService, TImpl>(
        this IServiceCollection services,
        object serviceKey)
        where TService : class
        where TImpl : class, TService
    {
        return services.Upsert(ServiceLifetime.Scoped, typeof(TService), serviceKey, typeof(TImpl));
    }

    /// <summary>
    ///     Adds or replaces all occurrences of <typeparamref name="TService" />
    ///     in provided service collection with Scoped lifetime using implementation factory
    /// </summary>
    /// <typeparam name="TService">Service type to replace</typeparam>
    /// <param name="services">Service collection</param>
    /// <param name="implementationFactory">Implementation factory</param>
    /// <returns>The modified service collection</returns>
    public static IServiceCollection UpsertScoped<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        return services.Upsert(
            ServiceLifetime.Scoped,
            typeof(TService),
            implementationFactory: implementationFactory
        );
    }

    /// <summary>
    ///     Adds or replaces all occurrences of <typeparamref name="TService" />
    ///     in provided service collection with Scoped lifetime using implementation factory and specified key
    /// </summary>
    /// <typeparam name="TService">Service type to replace</typeparam>
    /// <param name="services">Service collection</param>
    /// <param name="keyedImplementationFactory">Keyed implementation factory</param>
    /// <param name="serviceKey">Service key</param>
    /// <returns>The modified service collection</returns>
    public static IServiceCollection UpsertKeyedScoped<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, object?, TService> keyedImplementationFactory,
        object serviceKey)
        where TService : class
    {
        return services.Upsert(
            ServiceLifetime.Scoped,
            typeof(TService),
            serviceKey,
            keyedImplementationFactory: keyedImplementationFactory
        );
    }

    /// <summary>
    ///     Adds or replaces all occurrences of <typeparamref name="TService" />
    ///     in provided service collection with Transient lifetime
    /// </summary>
    /// <typeparam name="TService">Service type to replace</typeparam>
    /// <typeparam name="TImpl">Replacing type</typeparam>
    /// <param name="services">Service collection</param>
    /// <returns>The modified service collection</returns>
    public static IServiceCollection UpsertTransient<TService, TImpl>(this IServiceCollection services)
        where TService : class
        where TImpl : class, TService
    {
        return services.Upsert(ServiceLifetime.Transient, typeof(TService), implementationType: typeof(TImpl));
    }

    /// <summary>
    ///     Adds or replaces all occurrences of <typeparamref name="TService" />
    ///     in provided service collection with Transient lifetime and specified key
    /// </summary>
    /// <typeparam name="TService">Service type to replace</typeparam>
    /// <typeparam name="TImpl">Replacing type</typeparam>
    /// <param name="services">Service collection</param>
    /// <param name="serviceKey">Service key</param>
    /// <returns>The modified service collection</returns>
    public static IServiceCollection UpsertKeyedTransient<TService, TImpl>(
        this IServiceCollection services,
        object serviceKey)
        where TService : class
        where TImpl : class, TService
    {
        return services.Upsert(ServiceLifetime.Transient, typeof(TService), serviceKey, typeof(TImpl));
    }

    /// <summary>
    ///     Adds or replaces all occurrences of <typeparamref name="TService" />
    ///     in provided service collection with Transient lifetime using implementation factory
    /// </summary>
    /// <typeparam name="TService">Service type to replace</typeparam>
    /// <param name="services">Service collection</param>
    /// <param name="implementationFactory">Implementation factory</param>
    /// <returns>The modified service collection</returns>
    public static IServiceCollection UpsertTransient<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        return services.Upsert(
            ServiceLifetime.Transient,
            typeof(TService),
            implementationFactory: implementationFactory
        );
    }

    /// <summary>
    ///     Adds or replaces all occurrences of <typeparamref name="TService" />
    ///     in provided service collection with Transient lifetime using implementation factory and specified key
    /// </summary>
    /// <typeparam name="TService">Service type to replace</typeparam>
    /// <param name="services">Service collection</param>
    /// <param name="keyedImplementationFactory">Keyed implementation factory</param>
    /// <param name="serviceKey">Service key</param>
    /// <returns>The modified service collection</returns>
    public static IServiceCollection UpsertKeyedTransient<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, object?, TService> keyedImplementationFactory,
        object serviceKey)
        where TService : class
    {
        return services.Upsert(
            ServiceLifetime.Transient,
            typeof(TService),
            serviceKey,
            keyedImplementationFactory: keyedImplementationFactory
        );
    }

    /// <summary>
    ///     Adds or replaces all occurrences of service type in provided service collection
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="lifetime">Service lifetime</param>
    /// <param name="serviceType">Service type</param>
    /// <param name="serviceKey">Service key</param>
    /// <param name="implementationType">Implementation type</param>
    /// <param name="implementationInstance">Implementation instance</param>
    /// <param name="implementationFactory">Implementation factory</param>
    /// <param name="keyedImplementationFactory">Keyed implementation factory</param>
    /// <returns>The modified service collection</returns>
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
