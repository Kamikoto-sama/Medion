using Microsoft.Extensions.DependencyInjection;

namespace Medion.DynamicInjection;

public static class ServiceProviderExtensions
{
    /// <summary>
    ///     Retrieves a required service of type <typeparamref name="TService" /> and injects the specified argument into its
    ///     dependencies.
    /// </summary>
    /// <typeparam name="TService">The type of service to retrieve.</typeparam>
    /// <typeparam name="TArg">The type of argument to inject.</typeparam>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="arg">The argument to inject.</param>
    /// <returns>The requested service with the argument injected.</returns>
    public static TService GetRequiredServiceWithArgs<TService, TArg>(this IServiceProvider serviceProvider, TArg arg)
        where TService : notnull
    {
        var argContainer = serviceProvider.GetRequiredService<ArgContainer<TArg>>();
        argContainer.Arg.Value = arg;
        var service = serviceProvider.GetRequiredService<TService>();
        argContainer.Arg.Value = default;
        return service;
    }
}