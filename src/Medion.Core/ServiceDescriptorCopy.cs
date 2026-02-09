namespace Microsoft.Extensions.DependencyInjection.Medion.Core;

public class ServiceDescriptorCopy : ServiceDescriptor
{
    public ServiceDescriptorCopy(Type serviceType, Type implementationType, ServiceLifetime lifetime) : base(
        serviceType, implementationType, lifetime)
    {
    }

    public ServiceDescriptorCopy(Type serviceType, object? serviceKey, Type implementationType,
        ServiceLifetime lifetime) : base(serviceType, serviceKey, implementationType, lifetime)
    {
    }

    public ServiceDescriptorCopy(Type serviceType, object instance) : base(serviceType, instance)
    {
    }

    public ServiceDescriptorCopy(Type serviceType, object? serviceKey, object instance) : base(serviceType, serviceKey,
        instance)
    {
    }

    public ServiceDescriptorCopy(Type serviceType, Func<IServiceProvider, object> factory, ServiceLifetime lifetime) :
        base(serviceType, factory, lifetime)
    {
    }

    public ServiceDescriptorCopy(Type serviceType, object? serviceKey, Func<IServiceProvider, object?, object> factory,
        ServiceLifetime lifetime) : base(serviceType, serviceKey, factory, lifetime)
    {
    }
}