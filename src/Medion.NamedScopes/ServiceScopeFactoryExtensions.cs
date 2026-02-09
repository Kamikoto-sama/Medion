using Microsoft.Extensions.DependencyInjection;

namespace Medion.NamedScopes;

public static class ServiceScopeFactoryExtensions
{
    public static IServiceScope CreateNamedScope(this IServiceScopeFactory serviceScopeFactory, string scopeName)
    {
        var scope = serviceScopeFactory.CreateScope();
        var scopeNameContainer = scope.ServiceProvider.GetRequiredService<ScopeNameContainer>();
        scopeNameContainer.Name = scopeName;
        return scope;
    }
}