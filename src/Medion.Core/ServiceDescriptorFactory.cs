namespace Microsoft.Extensions.DependencyInjection.Medion.Core;

/// <summary>
///     Factory for creating and manipulating <see cref="ServiceDescriptor" /> instances.
/// </summary>
public static class ServiceDescriptorFactory
{
    /// <summary>
    ///     Creates a new <see cref="ServiceDescriptor" /> based on the specified parameters.
    /// </summary>
    /// <param name="lifetime">The lifetime of the service.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="serviceKey">The key for the service, if any.</param>
    /// <param name="implementationType">The implementation type.</param>
    /// <param name="implementationInstance">The implementation instance.</param>
    /// <param name="implementationFactory">The factory function for creating the implementation.</param>
    /// <param name="keyedImplementationFactory">The keyed factory function for creating the implementation.</param>
    /// <returns>A <see cref="ServiceDescriptor" /> configured according to the provided parameters.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no valid implementation configuration is provided.</exception>
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
        if (keyedImplementationFactory != null)
            return hasKey
                ? new ServiceDescriptor(serviceType, serviceKey,
                    keyedImplementationFactory, lifetime)
                : new ServiceDescriptor(serviceType,
                    s => keyedImplementationFactory(s, null), lifetime);

        throw new ArgumentException("At least one implementation must be specified.");
    }

    /// <summary>
    ///     Creates a new <see cref="ServiceDescriptor" /> by copying an existing one and overriding provided parameters.
    /// </summary>
    /// <param name="descriptor">The source <see cref="ServiceDescriptor" /> to copy.</param>
    /// <param name="lifetime">The new lifetime, or <c>null</c> to keep the existing one.</param>
    /// <param name="serviceKey">The new service key, or <c>null</c> to keep the existing one.</param>
    /// <param name="serviceType">The new service type, or <c>null</c> to keep the existing one.</param>
    /// <param name="implementationType">The new implementation type, or <c>null</c> to keep the existing one.</param>
    /// <param name="implementationInstance">The new implementation instance, or <c>null</c> to keep the existing one.</param>
    /// <param name="implementationFactory">The new implementation factory, or <c>null</c> to keep the existing one.</param>
    /// <param name="keyedImplementationFactory">The new keyed implementation factory, or <c>null</c> to keep the existing one.</param>
    /// <param name="removeKey">If <c>true</c>, removes the service key from the descriptor.</param>
    /// <returns>A new <see cref="ServiceDescriptor" /> with the specified changes applied.</returns>
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