namespace Microsoft.Extensions.DependencyInjection.Medion.Core;

public static class ServiceDescriptorFactory
{
    public static ServiceDescriptor Create(
        ServiceLifetime lifetime,
        Type serviceType,
        object? serviceKey = null,
        Type? implementationType = null,
        object? implementationInstance = null,
        Func<IServiceProvider, object>? implementationFactory = null,
        Func<IServiceProvider, object?, object>? keyedImplementationFactory = null)
    {
        var hasKey = serviceKey != null;
        if (implementationType != null)
            return hasKey
                ? new ServiceDescriptor(serviceType, serviceKey,
                    implementationType, lifetime)
                : new ServiceDescriptor(serviceType, implementationType,
                    lifetime);
        if (implementationInstance != null)
            return hasKey
                ? new ServiceDescriptor(serviceType, serviceKey,
                    implementationInstance)
                : new ServiceDescriptor(serviceType, implementationInstance);
        if (implementationFactory != null)
            return new ServiceDescriptor(serviceType, implementationFactory,
                lifetime);
        if (implementationFactory != null)
            return hasKey
                ? new ServiceDescriptor(serviceType, serviceKey,
                    (s, _) => implementationFactory(s), lifetime)
                : new ServiceDescriptor(serviceType, implementationFactory,
                    lifetime);
        if (keyedImplementationFactory != null)
            return hasKey
                ? new ServiceDescriptor(serviceType, serviceKey,
                    keyedImplementationFactory, lifetime)
                : new ServiceDescriptor(serviceType,
                    s => keyedImplementationFactory(s, null), lifetime);

        throw new Exception();
    }

    public static ServiceDescriptor CopyWith(
        this ServiceDescriptor descriptor,
        ServiceLifetime? lifetime = null,
        object? serviceKey = null,
        Type? serviceType = null,
        Type? implementationType = null,
        object? implementationInstance = null,
        Func<IServiceProvider, object>? implementationFactory = null,
        Func<IServiceProvider, object?, object>? keyedImplementationFactory = null,
        bool removeKey = false)
    {
        lifetime ??= descriptor.Lifetime;
        serviceType ??= descriptor.ServiceType;
        serviceKey ??= descriptor.ServiceKey;
        if (removeKey)
            serviceKey = null;

        var replaceImpl = implementationType != null ||
                          implementationInstance != null ||
                          implementationFactory != null ||
                          keyedImplementationFactory != null;

        if (!replaceImpl)
        {
            implementationType = descriptor.IsKeyedService
                ? descriptor.KeyedImplementationType
                : descriptor.ImplementationType;
            implementationInstance = descriptor.IsKeyedService
                ? descriptor.KeyedImplementationInstance
                : descriptor.ImplementationInstance;
            if (descriptor.IsKeyedService)
                keyedImplementationFactory = descriptor.KeyedImplementationFactory;
            else
                implementationFactory = descriptor.ImplementationFactory;
        }

        return Create(
            lifetime.Value,
            serviceType,
            serviceKey,
            implementationType,
            implementationInstance,
            implementationFactory,
            keyedImplementationFactory
        );
    }
}