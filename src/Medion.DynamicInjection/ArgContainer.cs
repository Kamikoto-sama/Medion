namespace Medion.DynamicInjection;

internal class ArgContainer<T>
{
    public AsyncLocal<T?> Arg { get; } = new();
}