using Medion.DynamicInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Sandbox;

internal static class Program
{
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddTransient<IServiceB, ServiceB>();
        services.AddTransient(typeof(IServiceA<>), typeof(ServiceA<>));
        services.AddAllDynamicInjectionArgTypes();

        var serviceProvider = services.BuildServiceProvider();
        var service = serviceProvider.GetRequiredServiceWithArgs<IServiceA<int>, string>("hello");
        Console.WriteLine(service.Name);
    }
}

public interface IServiceA<T>
{
    public string Name { get; }
}

public interface IServiceB;

public class ServiceB : IServiceB;

public class ServiceA<T>(IServiceB serviceB, [DynamicInjection] string name) : IServiceA<T>
{
    public string Name { get; } = name;
}