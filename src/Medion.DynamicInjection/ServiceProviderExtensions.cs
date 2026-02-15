using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Medion.DynamicInjection;

public static class ServiceProviderExtensions
{
    /// <summary>
    /// Retrieves the required service of type <typeparamref name="T" />
    /// injecting the specified arguments as dependencies.
    /// </summary>
    /// <typeparam name="T">The type of service to retrieve.</typeparam>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="args">The arguments to inject.</param>
    /// <returns>The requested service with the argument injected.</returns>
    public static T GetRequiredServiceWithArgs<T>(
        this IServiceProvider serviceProvider,
        params object[] args)
        where T : notnull
    {
        var containers = new List<IArgContainer>();
        var fillContainer = typeof(ServiceProviderExtensions)
            .GetMethod(nameof(FillContainer), BindingFlags.NonPublic | BindingFlags.Static)!;
        foreach (var arg in args)
            fillContainer.MakeGenericMethod(arg.GetType()).Invoke(null, [arg, serviceProvider, containers]);

        var service = serviceProvider.GetRequiredService<T>();
        containers.ForEach(c => c.ClearArg());
        return service;
    }

    private static void FillContainer<T>(T arg, IServiceProvider serviceProvider, List<IArgContainer> containers)
    {
        var container = serviceProvider.GetRequiredService<ArgContainer<T>>();
        container.Arg.Value = arg;
        containers.Add(container);
    }
}