using Microsoft.Extensions.DependencyInjection;

namespace Medion.DynamicServiceProvider;

public static class ServiceProviderExtensions
{
    /// <summary>
    ///     Checks if <paramref name="serviceProvider" /> is <see cref="DynamicServiceProvider" /> and
    ///     attempts to rebuild the service provider with new service configurations.
    /// </summary>
    /// <param name="serviceProvider">The service provider to rebuild.</param>
    /// <param name="configureServices">Action to configure the service collection.</param>
    /// <returns>
    ///     True if the provider was successfully rebuilt;
    ///     false if <paramref name="serviceProvider" /> is not <see cref="DynamicServiceProvider" />
    ///     or any scope has been already created with the underlying service provider.
    /// </returns>
    public static bool TryRebuild(this IServiceProvider serviceProvider, Action<IServiceCollection> configureServices)
    {
        return serviceProvider is DynamicServiceProvider dynamicServiceProvider &&
               dynamicServiceProvider.TryRebuild(configureServices);
    }
}