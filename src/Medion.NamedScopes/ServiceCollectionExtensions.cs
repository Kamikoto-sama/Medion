using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection.Medion.Core;

namespace Medion.NamedScopes;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection WithinNamedScope(
        this IServiceCollection services,
        string scopeName,
        Action<IServiceCollection> configure)
    {
        var scopedServices = GetScopedServices(scopeName, services);
        configure(scopedServices);
        foreach (var scopedService in scopedServices)
            if (scopedService.Lifetime == ServiceLifetime.Singleton)
                services.Add(scopedService);
            else if (scopedService is not ServiceDescriptorCopy)
                services.TryAddProxy(scopeName, scopedService);

        services.TryAddScoped<ScopeNameContainer>();
        return services;
    }

    private static ServiceCollection GetScopedServices(string scopeName, IServiceCollection services)
    {
        var scopedServices = new ServiceCollection();
        foreach (var service in services)
            if (service.Lifetime == ServiceLifetime.Singleton ||
                (service.ServiceKey is NamedScopeServiceKey nsServiceKey && nsServiceKey.ScopeName == scopeName))
                scopedServices.Add(service.CopyWith(removeKey: true));
        return scopedServices;
    }

    private static void TryAddProxy(this IServiceCollection services, string scopeName, ServiceDescriptor descriptor)
    {
        services.Add(descriptor.CopyWith(serviceKey: new NamedScopeServiceKey(scopeName, descriptor.ServiceKey)));
        services.TryAdd(descriptor.CopyWith(implementationFactory: sp =>
        {
            var scopeNameContainer = sp.GetRequiredService<ScopeNameContainer>();
            if (scopeNameContainer.Name == null)
                throw new InvalidOperationException();
            var serviceKey = new NamedScopeServiceKey(scopeNameContainer.Name, descriptor.ServiceKey);
            return sp.GetRequiredKeyedService(descriptor.ServiceType, serviceKey);
        }));
    }

    private static void TryAddEnumerableProxy(
        this IServiceCollection services,
        string scopeName,
        ServiceDescriptor descriptor)
    {
        var serviceType = typeof(IEnumerable<>).MakeGenericType(descriptor.ServiceType);
        services.TryAddProxy(scopeName, descriptor.CopyWith(serviceType: serviceType));
    }
}