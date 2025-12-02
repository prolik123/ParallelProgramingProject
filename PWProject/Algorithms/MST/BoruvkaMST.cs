using PWProject.Algorithms.FindUnion;
using PWProject.DataStructures;

namespace PWProject.Algorithms.MST;

public class BoruvkaMST : IMST
{
    private readonly IFindUnion _findUnion;

    public BoruvkaMST(int n) 
    {
        _findUnion = new FindUnionStructure(n);
    }

    public BoruvkaMST(IFindUnion findUnion)
    {
        _findUnion = findUnion;
    }

    public (IEnumerable<Edge<int>> edges, long cost) GetMST(List<List<Pair<Edge<int>, long>>> adjparam, int n)
    {
        var adj = new List<List<Pair<Edge<int>, long>>>(adjparam);
        if(n < 2)
            return ([], 0);
        
        var resultTree = new List<Edge<int>>();
        long cost = 0;
        while(true)
        {
            var dict = new Dictionary<int, Pair<Edge<int>, long>>();
            for (var k = 0; k < n; k++) 
            {
                var edge = FindMinimalEdgeForVertex(adj, k);
                if(edge is null)
                    continue;
                AddOrUpdate(dict, _findUnion.Find(k), edge);
            }
            
            if(dict.Count <= 1)
                break;
            
            foreach(var x in dict) 
            {
                var firstIdx = _findUnion.Find(x.Value.First.First);
                var secIdx = _findUnion.Find(x.Value.First.Second);
                if(firstIdx == secIdx)
                    continue;
                _findUnion.Union(firstIdx, secIdx);
                resultTree.Add(x.Value.First);
                cost += x.Value.Second;
            }
        }
        return (resultTree, cost);
    }

    private Pair<Edge<int>, long> FindMinimalEdgeForVertex(List<List<Pair<Edge<int>, long>>> adj, int x) 
    {
        var connectedNumber = _findUnion.Find(x);
        var edges = adj[x].Where(it => _findUnion.Find(it.First.Second) != connectedNumber).ToList();
        adj[x] = edges;
        if (edges.Count != 0) 
            return FindMinimalEdge(edges);
        
        if(adj[x].Count != 0)
            adj[x] = [];
        return null;
    }

    private static Pair<Edge<int>, long> FindMinimalEdge(IEnumerable<Pair<Edge<int>, long>> edges) 
        => edges.Aggregate((prev, next) => prev.Second > next.Second ? next : prev);

    private static void AddOrUpdate(IDictionary<int, Pair<Edge<int>, long>> dict, int idx, Pair<Edge<int>, long> value)
    {
        if(dict.TryGetValue(idx, out var val))
        {
            if(val.Second > value.Second)
                dict[idx] = value;
        }
        else
            dict.Add(idx, value);
    }
}