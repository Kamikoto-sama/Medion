namespace Medion.DynamicInjection;

internal interface IArgContainer
{
    void ClearArg();
}

internal class ArgContainer<T> : IArgContainer
{
    public AsyncLocal<T?> Arg { get; } = new();

    public void ClearArg()
    {
        Arg.Value = default;
    }
}