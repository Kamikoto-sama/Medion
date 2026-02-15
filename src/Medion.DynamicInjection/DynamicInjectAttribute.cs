using Microsoft.Extensions.DependencyInjection;

namespace Medion.DynamicInjection;

/// <summary>
/// Attribute used to indicate that a parameter will be injected dynamically during resolving.
/// </summary>
public class DynamicInjectAttribute() : FromKeyedServicesAttribute(new DynamicInjectionServiceKey());