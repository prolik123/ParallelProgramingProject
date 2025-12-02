using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PWProject.Algorithms.FindUnion;
using PWProject.DataStructures;

namespace PWProject.Algorithms.MST;

public class FastBoruvkaMST : IMST
{
    private readonly IFindUnion _findUnion;
    private readonly int _maxThreads;

    public FastBoruvkaMST(int n, int threads) 
    {
        _findUnion = new FindUnionStructure(n);
        _maxThreads = threads;
    }
    
    public FastBoruvkaMST(IFindUnion findUnion, int threads) 
    {
        _findUnion = findUnion ?? throw new ArgumentNullException(nameof(findUnion));
        _maxThreads = threads;
    }

    public (IEnumerable<Edge<int>> edges, long cost) GetMST(List<List<Pair<Edge<int>, long>>> adjParam, int n)
    {
        var adj = new List<List<Pair<Edge<int>, long>>>(adjParam);
        if(n < 2)
            return ([], 0);
        
        var resultTree = new ConcurrentBag<Edge<int>>();
        long cost = 0 ;
        while(true)
        {
            var dict = new ConcurrentDictionary<int, Pair<Edge<int>, long>>();
            var parallelOptions =  new ParallelOptions { MaxDegreeOfParallelism = _maxThreads };
            Parallel.For(0, n, parallelOptions, k => ConsiderVertex(adj, k, dict));
            if(dict.Count < 2)
                break;
            dict.AsParallel().ForAll(x => MergeComponents(x, ref cost, resultTree));
        }
        return (resultTree, cost);
    }

    private void ConsiderVertex(List<List<Pair<Edge<int>, long>>> adj, int k, ConcurrentDictionary<int, Pair<Edge<int>, long>> dict) 
    {
        var edge = FindMinimalEdgeForVertex(adj, k);
        if(edge is null)
            return;
        AddOrUpdate(dict, _findUnion.Find(k), edge);
    }
    private Pair<Edge<int>, long> FindMinimalEdgeForVertex(List<List<Pair<Edge<int>, long>>> adj, int x) 
    {
        var connectedNumber = _findUnion.Find(x);
        
        var edges = adj[x]
            .Where(it => _findUnion.Find(it.First.Second) != connectedNumber)
            .ToList();
        
        if(edges.Count == 0)
            return null;
        adj[x] = edges;
        return FindMinimalEdge(edges);     
    }

    private static Pair<Edge<int>, long> FindMinimalEdge(IEnumerable<Pair<Edge<int>, long>> edges) 
        => edges.Aggregate((prev, next) => prev.Second > next.Second ? next : prev);

    private static void AddOrUpdate(ConcurrentDictionary<int, Pair<Edge<int>, long>> dict, int idx, Pair<Edge<int>, long> value)
    {
        dict.AddOrUpdate(idx, value, (_, y) => Swapper(y, value));
    }

    private static Pair<Edge<int>, long> Swapper(Pair<Edge<int>, long> a, Pair<Edge<int>, long> b) => a.Second > b.Second ? b : a;
    
    private void MergeComponents(KeyValuePair<int, Pair<Edge<int>, long>> x, ref long cost, ConcurrentBag<Edge<int>> resultTree) 
    {
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