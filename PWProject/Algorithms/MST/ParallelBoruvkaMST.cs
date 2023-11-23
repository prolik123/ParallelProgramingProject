
using DataStructures;
using Algorithms.FindUnion;

namespace Algorithms.MST;

public class ParallelBoruvkaMST : IMST
{
    private StarFindUnion _findUnion;
    private List<List<Pair<Edge<int>, long>>> _adj;
    private int n;
    private List<Pair<Edge<int>, long>> _list;
    private List<Edge<int>> resultTree;
    private long cost;
    private bool isEdge;

    private int MAX_THREADS;

    public ParallelBoruvkaMST(List<List<Pair<Edge<int>, long>>> adj, int n, int threads) 
    {
        _adj = new (adj);
        this.n = n;
        MAX_THREADS = threads;
    }

    public (IEnumerable<Edge<int>> edges, long cost) GetMST()
    {
        if(n < 2)
            return (Enumerable.Empty<Edge<int>>(), 0);
        resultTree = new List<Edge<int>>();
        _findUnion = new StarFindUnion(n);
        _list = new List<Pair<Edge<int>, long>>();
        for(int k=0;k<n;k++)
            _list.Add(null);
        cost = 0;
        while(true)
        {
            isEdge = false;
            Parallel.For(0, n, new ParallelOptions { MaxDegreeOfParallelism = MAX_THREADS}, ConsiderVertex);
            if(!isEdge)
                break;
            Parallel.For(0, n, new ParallelOptions { MaxDegreeOfParallelism = MAX_THREADS}, ComposeComponent);
            Parallel.For(0, n, new ParallelOptions { MaxDegreeOfParallelism = MAX_THREADS}, MergeComps);
        }
        return (resultTree, cost);
    }

    private void MergeComps(int x) 
    {
        if(_list[x] is null)
            return;
        MergeComponents(x);
        _list[x] = null;
    }

    private void ComposeComponent(int k) 
    {
        if(_findUnion.Find(k) == k) 
        {
            var members = _findUnion.GetMembers(k);
            var min = k;
            foreach(var member in members)
            {
                if(_list[member] is null)
                    continue;
                if(_list[min] is null || _list[min].Second > _list[member].Second)
                {
                    _list[min] = null;
                    min = member;
                }
                else if(min != member)
                    _list[member] = null;
            }
        }
    }

    private void ConsiderVertex(int k) 
    {
        var edge = FindMinimalEdgeForVertex(k);
        if(edge is null)
            return;
        _list[k] = edge;
    }
    private Pair<Edge<int>, long> FindMinimalEdgeForVertex(int x) 
    {
        var conectedNumber = _findUnion.Find(x); // for speed
        // Where() here do not slow it down
        var edges = _adj[x].Where(x => _findUnion.Find(x.First.Second) != conectedNumber).ToList();
        if(edges is null || !edges.Any()) 
        {
            if(_adj[x].Any())
                _adj[x] = new();
            return null;
        }
        isEdge = true;
        _adj[x] = edges;
        return FindMinimalEdge(edges);    
    }

    private Pair<Edge<int>, long> FindMinimalEdge(IEnumerable<Pair<Edge<int>, long>> edges) 
        => edges.Aggregate((prev, next) => prev.Second > next.Second ? next : prev);

    private void MergeComponents(int x) 
    {
        var pair = _list[x];
        lock(_findUnion) 
        {
            var firstIdx = _findUnion.Find(pair.First.First);
            var secIdx = _findUnion.Find(pair.First.Second);
            if(firstIdx == secIdx)
                return;
            _findUnion.Union(pair.First.First, pair.First.Second);
            resultTree.Add(pair.First);
            cost += pair.Second;
        }
    }
}