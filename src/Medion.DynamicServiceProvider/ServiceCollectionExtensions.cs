using Microsoft.Extensions.DependencyInjection;

namespace Medion.DynamicServiceProvider;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Builds a dynamic service provider with the specified options.
    /// Which can be rebuilt with different services afterward
    /// </summary>
    /// <param name="services">The service collection to build from.</param>
    /// <param name="options">The service provider options.</param>
    /// <returns>A new dynamic service provider.</returns>
    public static DynamicServiceProvider BuildDynamicServiceProvider(
        this IServiceCollection services,
        ServiceProviderOptions options)
    {
        return new DynamicServiceProvider(services, options);
    }

    /// <summary>
    /// Builds a dynamic service provider with default options.
    /// </summary>
    /// <param name="services">The service collection to build from.</param>
    /// <returns>A new dynamic service provider.</returns>
    public static DynamicServiceProvider BuildDynamicServiceProvider(this IServiceCollection services)
    {
        return services.BuildDynamicServiceProvider(new ServiceProviderOptions());
    }
}