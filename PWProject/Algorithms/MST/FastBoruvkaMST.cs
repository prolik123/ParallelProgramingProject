
using DataStructures;
using Algorithms.FindUnion;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Algorithms.MST;

public class FastBoruvkaMST : IMST
{
    private IFindUnion _findUnion;
    private List<List<Pair<Edge<int>, long>>> _adj;
    private int n;

    public FastBoruvkaMST(List<List<Pair<Edge<int>, long>>> adj, int n) 
    {
        _adj = new (adj);
        this.n = n;
        _findUnion = new FindUnionStructure(n);
    }

    public (IEnumerable<Edge<int>> edges, long cost) GetMST()
    {
        if(n < 2)
            return (Enumerable.Empty<Edge<int>>(), 0);
        var resultTree = new ConcurrentBag<Edge<int>>();
        long cost = 0 ;
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        while(true)
        {
            ConcurrentDictionary<int, Pair<Edge<int>, long>> dict = new ConcurrentDictionary<int, Pair<Edge<int>, long>>();
            Parallel.For(0, n, k => ConsiderVertex(k, dict));
            if(dict.Count < 2)
                break;
            dict.AsParallel().ForAll(x => MergeComponents(x, ref cost, resultTree));
        }
        stopwatch.Stop();
        return (resultTree, cost);
    }

    private void ConsiderVertex(int k, ConcurrentDictionary<int, Pair<Edge<int>, long>> dict) 
    {
        var edge = FindMinimalEdgeForVertex(k);
        if(edge is null)
            return;
        AddOrUpdate(dict, _findUnion.Find(k), edge);
    }
    private Pair<Edge<int>, long> FindMinimalEdgeForVertex(int x) 
    {
        var conectedNumber = _findUnion.Find(x); // for speed
        // Where() here do not slow it down
        var edges = _adj[x].Where(x => _findUnion.Find(x.First.Second) != conectedNumber).ToList();
        if(edges is null || !edges.Any())
            return null;
        _adj[x] = edges;
        return FindMinimalEdge(edges);     
    }

    private Pair<Edge<int>, long> FindMinimalEdge(IEnumerable<Pair<Edge<int>, long>> edges) 
        => edges.Aggregate((prev, next) => prev.Second > next.Second ? next : prev);

    private void AddOrUpdate(ConcurrentDictionary<int, Pair<Edge<int>, long>> dict, int idx, Pair<Edge<int>, long> value)
    {
        dict.AddOrUpdate(idx, value, (x, y) => Swapper(y, value));
    }

    private Pair<Edge<int>, long> Swapper(Pair<Edge<int>, long> a, Pair<Edge<int>, long> b) => a.Second > b.Second ? b : a;
    private void MergeComponents(KeyValuePair<int, Pair<Edge<int>, long>> x, ref long cost, ConcurrentBag<Edge<int>> resultTree) 
    {
        // Tu jest problem z tym lockiem
        lock(_findUnion) 
        {
            var firstIdx = _findUnion.Find(x.Value.First.First);
            var secIdx = _findUnion.Find(x.Value.First.Second);
            if(firstIdx == secIdx)
                return;
            _findUnion.Union(x.Value.First.First, x.Value.First.Second);
        }
        resultTree.Add(x.Value.First);
        Interlocked.Add(ref cost, x.Value.Second);
    }
    
}