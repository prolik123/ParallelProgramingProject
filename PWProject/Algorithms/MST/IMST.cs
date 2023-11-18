using DataStructures;

namespace Algorithms.MST;
public interface IMST 
{
    public (IEnumerable<Edge<int>> edges, long cost) GetMST(IEnumerable<IEnumerable<Pair<Edge<int>, int>>> adj, int n, int m);
}