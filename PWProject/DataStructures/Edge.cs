
namespace DataStructures;

public class Edge<T> : Pair<T, T>
{
    public Edge(T first, T second){
        First = first;
        Second = second;
    }
}