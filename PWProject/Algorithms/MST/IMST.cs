using PWProject.DataStructures;

namespace PWProject.Algorithms.MST;
public interface IMST 
{
    public (IEnumerable<Edge<int>> edges, long cost) GetMST(List<List<Pair<Edge<int>, long>>> adj, int n);
}