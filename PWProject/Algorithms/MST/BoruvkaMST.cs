using DataStructures;

namespace Algorithms.MST;

public class BoruvkaMST : IMST
{
    public (IEnumerable<Edge<int>> edges, long cost) GetMST(IEnumerable<IEnumerable<Pair<Edge<int>, int>>> adj, int n, int m)
    {
        return (new List<Edge<int>>(), 0);
    }
}