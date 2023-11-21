
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

    public ParallelBoruvkaMST(List<List<Pair<Edge<int>, long>>> adj, int n) 
    {
        _adj = adj;
        this.n = n;
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
            Parallel.For(0, n, k => ConsiderVertex(k));
            if(!isEdge)
                break;
            Parallel.For(0, n, (k) => ComposeComponent(k));
            Parallel.For(0, n, (x) => MergeComponents(x));
        }
        return (resultTree, cost);
    }

    private void ComposeComponent(int k) 
    {
        if(_findUnion.Find(k) != k)
            return;
        var members = _findUnion.GetMembers(k);
        var min = k;
        foreach(var member in members)
        {
            if(_list[min].Second > _list[member].Second)
            {
                _list[min] = null;
                min = member;
            }
            else if(min != member)
                _list[member] = null;
        }
    }

    private void ConsiderVertex(int k) 
    {
        var edge = FindMinimalEdgeForVertex(k);
        if(edge is null)
            return;
        isEdge = true;
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
        _adj[x] = edges;
        return FindMinimalEdge(edges);     
    }

    private Pair<Edge<int>, long> FindMinimalEdge(IEnumerable<Pair<Edge<int>, long>> edges) 
        => edges.Aggregate((prev, next) => prev.Second > next.Second ? next : prev);

    private void MergeComponents(int x) 
    {
        // Tu jest problem z tym lockiem
        if(_list[x] is null)
            return;
        lock(_findUnion) 
        {
            var firstIdx = _findUnion.Find(_list[x].First.First);
            var secIdx = _findUnion.Find(_list[x].First.Second);
            if(firstIdx == secIdx)
                return;
            _findUnion.Union(_list[x].First.First, _list[x].First.Second);
            resultTree.Add(_list[x].First);
            cost += _list[x].Second;
        }
        _list[x] = null;
    }
    
}