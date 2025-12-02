using PWProject.Algorithms.FindUnion;
using PWProject.DataStructures;

namespace PWProject.Algorithms.MST;

public class ParallelBoruvkaMST : IMST
{
    private readonly StarFindUnion _findUnion;

    private readonly int _maxThreads;

    public ParallelBoruvkaMST(int maxThreads, int n) 
    {
        _findUnion = new StarFindUnion(n);
        _maxThreads = maxThreads;
    }

    public ParallelBoruvkaMST(StarFindUnion findUnion, int maxThreads)
    {
        _findUnion = findUnion ?? throw new ArgumentNullException(nameof(findUnion));
        _maxThreads = maxThreads;
    }

    public (IEnumerable<Edge<int>> edges, long cost) GetMST(List<List<Pair<Edge<int>, long>>> adjParam, int n)
    {
        if(n < 2)
            return ([], 0);
        
        var adj = new  List<List<Pair<Edge<int>, long>>>(adjParam);
        List<Edge<int>> resultTree = [];
        
        List<Pair<Edge<int>, long>> list = [];
        for (var k = 0; k < n; k++)
            list.Add(null);
        
        long cost = 0;
        while(true)
        {
            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = _maxThreads };
            var isEdge = false;
            Parallel.For(0, n, parallelOptions, (x) => ConsiderVertex(adj, list, x, ref isEdge));
            if(!isEdge)
                break;
            
            Parallel.For(0, n, parallelOptions, (x) => ComposeComponent(list, x));
            Parallel.For(0, n, parallelOptions, (x) => MergeComps(resultTree, list, x, ref cost));
        }
        return (resultTree, cost);
    }

    private void MergeComps(List<Edge<int>> resultTree, List<Pair<Edge<int>, long>> list, int x, ref long cost) 
    {
        if(list[x] is null)
            return;
        
        MergeComponents(resultTree, list, x, ref cost);
        list[x] = null;
    }

    private void ComposeComponent(List<Pair<Edge<int>, long>> list, int k)
    {
        if (_findUnion.Find(k) != k) 
            return;
        
        var members = _findUnion.GetMembers(k);
        var min = k;
        foreach (var member in members.Where(member => list[member] is not null))
        {
            if(list[min] is null || list[min].Second > list[member].Second)
            {
                list[min] = null;
                min = member;
            }
            else if(min != member)
                list[member] = null;
        }
    }

    private void ConsiderVertex(List<List<Pair<Edge<int>, long>>> adj, List<Pair<Edge<int>, long>> list, int k, ref bool isEdge) 
    {
        var edge = FindMinimalEdgeForVertex(adj, k, ref isEdge);
        if(edge is null)
            return;
        
        list[k] = edge;
    }
    private Pair<Edge<int>, long> FindMinimalEdgeForVertex(List<List<Pair<Edge<int>, long>>> adj, int x, ref bool isEdge) 
    {
        var connectedNumber = _findUnion.Find(x);
        var edges = adj[x]
            .Where(it => _findUnion.Find(it.First.Second) != connectedNumber)
            .ToList();
        
        if(edges.Count == 0) 
        {
            if(adj[x].Count != 0)
                adj[x] = [];
            return null;
        }
        
        isEdge = true;
        adj[x] = edges;
        return FindMinimalEdge(edges);    
    }

    private static Pair<Edge<int>, long> FindMinimalEdge(IEnumerable<Pair<Edge<int>, long>> edges) 
        => edges.Aggregate((prev, next) => prev.Second > next.Second ? next : prev);

    private void MergeComponents(List<Edge<int>> resultTree, List<Pair<Edge<int>, long>> list, int x, ref long cost) 
    {
        var pair = list[x];
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