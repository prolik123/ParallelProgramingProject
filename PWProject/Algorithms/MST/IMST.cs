using DataStructures;

namespace Algorithms.MST;
public interface IMST 
{
    public (IEnumerable<Edge<int>> edges, long cost) GetMST(List<List<Pair<Edge<int>, long>>> adj, int n, int m);
}