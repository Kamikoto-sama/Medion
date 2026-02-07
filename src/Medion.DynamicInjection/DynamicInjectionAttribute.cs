using Microsoft.Extensions.DependencyInjection;

namespace Medion.DynamicInjection;

/// <summary>
///     Attribute used to indicate that a parameter should be injected using the dynamic injection mechanism.
/// </summary>
public class DynamicInjectionAttribute() : FromKeyedServicesAttribute(new DynamicInjectionServiceKey());