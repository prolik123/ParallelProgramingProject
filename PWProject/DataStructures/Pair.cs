
namespace DataStructures;

public class Pair<T,S>(T first = default, S second = default)
{
    public T First { get; set; } = first;
    public S Second { get; set; } = second;
}