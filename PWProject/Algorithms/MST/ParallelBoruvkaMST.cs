
using DataStructures;

namespace Algorithms.MST;

public class ParallelBoruvkaMST : IMST
{
    public (IEnumerable<Edge<int>> edges, long cost) GetMST(List<List<Pair<Edge<int>, long>>> adj, int n, int m)
    {
        return (new List<Edge<int>>(), 0);
    }
}