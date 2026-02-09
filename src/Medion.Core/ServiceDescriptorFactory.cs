namespace Microsoft.Extensions.DependencyInjection.Medion.Core;

/// <summary>
/// Factory for creating and manipulating <see cref="ServiceDescriptor" /> instances.
/// </summary>
public static class ServiceDescriptorFactory
{
    /// <summary>
    /// Creates a new <see cref="ServiceDescriptor" /> based on the specified parameters.
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
                ? new ServiceDescriptorCopy(serviceType, serviceKey,
                    implementationType, lifetime)
                : new ServiceDescriptorCopy(serviceType, implementationType,
                    lifetime);
        if (implementationInstance != null)
            return hasKey
                ? new ServiceDescriptorCopy(serviceType, serviceKey,
                    implementationInstance)
                : new ServiceDescriptorCopy(serviceType, implementationInstance);
        if (implementationFactory != null)
            return hasKey
                ? new ServiceDescriptorCopy(serviceType, serviceKey, (s, _) => implementationFactory(s), lifetime)
                : new ServiceDescriptorCopy(serviceType, implementationFactory, lifetime);
        if (keyedImplementationFactory != null)
            return hasKey
                ? new ServiceDescriptorCopy(serviceType, serviceKey, keyedImplementationFactory, lifetime)
                : new ServiceDescriptorCopy(serviceType, s => keyedImplementationFactory(s, null), lifetime);

        throw new InvalidOperationException("At least one implementation must be specified.");
    }

    /// <summary>
    /// Creates a new <see cref="ServiceDescriptor" /> by copying an existing one and overriding provided parameters.
    /// </summary>
    /// <param name="descriptor">The source <see cref="ServiceDescriptor" /> to copy.</param>
    /// <param name="lifetime">The new lifetime.</param>
    /// <param name="serviceKey">
    /// The new service key.
    /// Turns not keyed service into keyed one, replacing implementation* with keyedImplementation* 
    /// </param>
    /// <param name="serviceType">The new service type.</param>
    /// <param name="implementationType">
    /// The new implementation type. Will be used as keyedImplementationType,
    /// if <paramref name="descriptor"/> is keyed or <paramref name="serviceKey"/> provided
    /// </param>
    /// <param name="implementationInstance">
    /// The new implementation instance. Will be used as keyedImplementationInstance,
    /// if <paramref name="descriptor"/> is keyed or <paramref name="serviceKey"/> provided
    /// </param>
    /// <param name="implementationFactory">
    /// The new implementation factory. Will be used as keyedImplementationFactory,
    /// if <paramref name="descriptor"/> is keyed or <paramref name="serviceKey"/> provided
    /// </param>
    /// <param name="keyedImplementationFactory">
    /// The new keyed implementation factory. Will be used as implementationFactory,
    /// if <paramref name="descriptor"/> is not keyed or <paramref name="removeKey"/> is <c>true</c>
    /// </param>
    /// <param name="removeKey">
    /// If <c>true</c>, removes the service key from the descriptor.
    /// Turns keyed service into not keyed, replacing keyedImplementation* with implementation*
    /// </param>
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