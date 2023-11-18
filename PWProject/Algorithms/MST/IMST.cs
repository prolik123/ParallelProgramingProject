using DataStructures;

namespace Algorithms.MST;
public interface IMST 
{
    public (List<Edge<int>> edges, long cost) GetMST(List<Pair<Edge<int>, int>> adj, int n, int m);
}