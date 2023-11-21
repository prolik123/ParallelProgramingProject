using System.Diagnostics;
using Algorithms.FindUnion;
using DataStructures;

namespace Algorithms.MST;

public class BoruvkaMST : IMST
{
    private IFindUnion _findUnion;
    private List<List<Pair<Edge<int>, long>>> _adj;
    private int n;

    public BoruvkaMST(List<List<Pair<Edge<int>, long>>> adj, int n) 
    {
        _adj = adj;
        this.n = n;
        _findUnion = new FindUnionStructure(n);
    }

    public (IEnumerable<Edge<int>> edges, long cost) GetMST()
    {
        if(n < 2)
            return (Enumerable.Empty<Edge<int>>(), 0);
        var resultTree = new List<Edge<int>>();
        long cost = 0;
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        while(true)
        {
            Dictionary<int, Pair<Edge<int>, long>> dict = new Dictionary<int, Pair<Edge<int>, long>>();
            for(int k=0;k<n;k++) 
            {
                var edge = FindMinimalEdgeForVertex(k);
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
        stopwatch.Stop();
        return (resultTree, cost);
    }

    private Pair<Edge<int>, long> FindMinimalEdgeForVertex(int x) 
    {
        var conectedNumber = _findUnion.Find(x); // for speed
        // Where() here do not slow it down
        var edges = _adj[x].Where(x => _findUnion.Find(x.First.Second) != conectedNumber).ToList();
        _adj[x] = edges;
        if(edges is null || !edges.Any())
            return null;
        return FindMinimalEdge(edges);     
    }

    private Pair<Edge<int>, long> FindMinimalEdge(IEnumerable<Pair<Edge<int>, long>> edges) 
        => edges.Aggregate((prev, next) => prev.Second > next.Second ? next : prev);

    private void AddOrUpdate(IDictionary<int, Pair<Edge<int>, long>> dict, int idx, Pair<Edge<int>, long> value)
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