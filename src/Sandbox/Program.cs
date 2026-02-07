using Medion.DynamicInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Sandbox;

internal static class Program
{
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddTransient(typeof(IService<>), typeof(Service<>));
        services.AddAllDynamicInjectionArgTypes();

        var serviceProvider = services.BuildServiceProvider();
        var service = serviceProvider.GetRequiredServiceWithArgs<IService<int>, string>("hello");
        Console.WriteLine(service.Name);
    }
}

public interface IService<T>
{
    public string Name { get; }
}

public class Service<T>([DynamicInjection] string name) : IService<T>
{
    public string Name { get; } = name;
}