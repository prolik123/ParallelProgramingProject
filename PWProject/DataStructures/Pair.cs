
namespace PWProject.DataStructures;

public record Pair<T,TS>(T First = default, TS Second = default)
{
    public T First { get; } = First;
    public TS Second { get; } = Second;
}