
using DataStructures;
using Algorithms.FindUnion;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Algorithms.MST;

public class ParallelBoruvkaMST : IMST
{
    private IFindUnion _findUnion;
    private List<List<Pair<Edge<int>, long>>> _adj;

    public (IEnumerable<Edge<int>> edges, long cost) GetMST(List<List<Pair<Edge<int>, long>>> adj, int n, int m)
    {
        if(n < 2)
            return (Enumerable.Empty<Edge<int>>(), 0);
        _findUnion = new FindUnionStructure(n);
        _adj = adj;
        var resultTree = new ConcurrentBag<Edge<int>>();
        long cost = 0 ;
        bool isSingleComponent;
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        do
        {
            ConcurrentDictionary<int, Pair<Edge<int>, long>> dict = new ConcurrentDictionary<int, Pair<Edge<int>, long>>();
            Enumerable.Range(0, n).AsParallel().ForAll(k => ConsiderVertex(k, dict));
            dict.AsParallel().ForAll(x => MergeComponents(x, ref cost, resultTree));

            isSingleComponent = dict.Count < 2;
        }while(!isSingleComponent);
        stopwatch.Stop();
        Console.WriteLine($"Parallel time: {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine(resultTree.Count);
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
        var edges = _adj[conectedNumber].Where(x => _findUnion.Find(x.First.Second) != conectedNumber);
        if(edges is null || !edges.Any())
            return null;
        return FindMinimalEdge(edges);     
    }

    private Pair<Edge<int>, long> FindMinimalEdge(IEnumerable<Pair<Edge<int>, long>> edges) 
        => edges.Aggregate((prev, next) => prev.Second > next.Second ? next : prev);

    private void AddOrUpdate(ConcurrentDictionary<int, Pair<Edge<int>, long>> dict, int idx, Pair<Edge<int>, long> value)
    {
        dict.AddOrUpdate(idx, value, (x, y) => value);
    }

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