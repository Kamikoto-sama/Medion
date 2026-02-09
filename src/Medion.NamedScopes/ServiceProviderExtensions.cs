using Microsoft.Extensions.DependencyInjection;

namespace Medion.NamedScopes;

public static class ServiceProviderExtensions
{
    public static IServiceScope CreateNamedScope(this IServiceProvider serviceProvider, string scopeName)
    {
        return serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateNamedScope(scopeName);
    }
}