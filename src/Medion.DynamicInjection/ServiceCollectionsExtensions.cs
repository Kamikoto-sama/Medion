using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Medion.DynamicInjection;

public static class ServiceCollectionsExtensions
{
    /// <summary>
    ///     Adds a dynamic injection argument type to the service collection.
    /// </summary>
    /// <typeparam name="T">The type of the argument.</typeparam>
    /// <param name="services">The service collection.</param>
    public static void AddDynamicInjectionArgType<T>(this IServiceCollection services) where T : class
    {
        services.TryAddSingleton<ArgContainer<T>>();
        services.AddKeyedTransient<T>(new DynamicInjectionServiceKey(), (sp, _) =>
        {
            var argContainer = sp.GetRequiredService<ArgContainer<T>>();
            return argContainer.Arg.Value ?? throw new InvalidOperationException();
        });
    }

    /// <summary>
    ///     Adds all dynamic injection argument types found in the service collection to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <remarks>Must be called last before building the container</remarks>
    public static void AddAllDynamicInjectionArgTypes(this IServiceCollection services)
    {
        var dynamicInjectionTypes = services
            .Select(s => s.IsKeyedService ? s.KeyedImplementationType : s.ImplementationType)
            .Where(t => t != null)
            .SelectMany(t => t!.GetConstructors()
                .SelectMany(c => c.GetParameters())
                .Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(DynamicInjectionAttribute)))
                .Select(p => p.ParameterType))
            .ToArray();

        var sceType = typeof(ServiceCollectionsExtensions);
        var addDynamicInjectionArgTypeMethod = sceType.GetMethod(nameof(AddDynamicInjectionArgType))!;
        foreach (var dynamicInjectionType in dynamicInjectionTypes)
            addDynamicInjectionArgTypeMethod.MakeGenericMethod(dynamicInjectionType).Invoke(null, [services]);
    }
}