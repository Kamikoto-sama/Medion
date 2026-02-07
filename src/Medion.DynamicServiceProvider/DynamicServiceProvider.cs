using Microsoft.Extensions.DependencyInjection;

namespace Medion.DynamicServiceProvider;

/// <summary>
///     A service provider that can be dynamically rebuilt with new service configurations.
/// </summary>
/// <param name="services">The service collection to build the provider from.</param>
/// <param name="options">The options for building the service provider.</param>
public class DynamicServiceProvider(IServiceCollection services, ServiceProviderOptions options)
    : IKeyedServiceProvider, IServiceScopeFactory, IDisposable, IAsyncDisposable
{
    private ServiceProvider serviceProvider = services.BuildServiceProvider(options);
    private bool scopeCreated;

    /// <summary>
    ///     Gets a service of the specified type.
    /// </summary>
    /// <param name="serviceType">The type of service to get.</param>
    /// <returns>The service instance, or null if the service is not found.</returns>
    public object? GetService(Type serviceType)
    {
        return serviceType == typeof(IServiceScopeFactory) ? this : serviceProvider.GetService(serviceType);
    }

    /// <summary>
    ///     Gets a keyed service of the specified type.
    /// </summary>
    /// <param name="serviceType">The type of service to get.</param>
    /// <param name="serviceKey">The key of the service to get.</param>
    /// <returns>The service instance, or null if the service is not found.</returns>
    public object? GetKeyedService(Type serviceType, object? serviceKey)
    {
        return serviceProvider.GetKeyedService(serviceType, serviceKey);
    }

    /// <summary>
    ///     Gets a required keyed service of the specified type.
    /// </summary>
    /// <param name="serviceType">The type of service to get.</param>
    /// <param name="serviceKey">The key of the service to get.</param>
    /// <returns>The service instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the service is not found.</exception>
    public object GetRequiredKeyedService(Type serviceType, object? serviceKey)
    {
        return serviceProvider.GetRequiredKeyedService(serviceType, serviceKey);
    }

    /// <summary>
    ///     Creates a new service scope.
    /// </summary>
    /// <returns>A new service scope.</returns>
    public IServiceScope CreateScope()
    {
        scopeCreated = true;
        return serviceProvider.CreateScope();
    }

    /// <summary>
    ///     Disposes the service provider.
    /// </summary>
    public void Dispose()
    {
        serviceProvider.Dispose();
    }

    /// <summary>
    ///     Asynchronously disposes the service provider.
    /// </summary>
    /// <returns>A ValueTask representing the asynchronous operation.</returns>
    public async ValueTask DisposeAsync()
    {
        await serviceProvider.DisposeAsync();
    }

    /// <summary>
    ///     Attempts to rebuild the service provider with new service configurations.
    /// </summary>
    /// <param name="configureServices">Action to configure the service collection.</param>
    /// <returns>
    ///     True if the provider was successfully rebuilt;
    ///     false if any scope has been already created by the underlying service provider.
    /// </returns>
    public bool TryRebuild(Action<IServiceCollection> configureServices)
    {
        if (scopeCreated)
            return false;
        configureServices(services);
        serviceProvider = services.BuildServiceProvider(options);
        return true;
    }
}