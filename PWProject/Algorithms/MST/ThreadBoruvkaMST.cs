using PWProject.Algorithms.FindUnion;
using PWProject.DataStructures;

namespace PWProject.Algorithms.MST;

public class ThreadBoruvkaMST : IMST
{
    private readonly int _threadNum;
    private readonly StarFindUnion _findUnion;

    public ThreadBoruvkaMST(int n, int threadNum) 
    {
        _findUnion = new StarFindUnion(n);
        _threadNum = threadNum;
    }

    public ThreadBoruvkaMST(StarFindUnion findUnion, int threadNum) 
    {
        _findUnion = findUnion ?? throw new ArgumentNullException(nameof(findUnion));
        _threadNum = threadNum;
    }
    
    public (IEnumerable<Edge<int>> edges, long cost) GetMST(List<List<Pair<Edge<int>, long>>> adj, int n)
    {
        if(n < 2)
            return ([], 0);
        
        var success = false;
        List<Edge<int>> resultTree = [];
        long cost = 0 ;
        List<Pair<Edge<int>, long>> list = [];
        
        for (var k = 0; k < n; k++)
            list.Add(new Pair<Edge<int>, long>(new Edge<int>(-1,-1),-1));
        
        var isEdge = false;
        var threads = new Thread[_threadNum];
        var inBarrier = new Barrier(_threadNum, (_) => isEdge = false);
        var outBarrier = new Barrier(_threadNum, (_) => success = !isEdge);
        var midBarrier = new Barrier(_threadNum);
        
        for (var k = 0; k < _threadNum; k++) 
        {
            var specInt = k;
            threads[k] = new Thread(() => ThreadFunction(adj, list, resultTree, n, specInt, inBarrier,
                outBarrier, midBarrier, ref cost, ref isEdge, ref success));
        }
        
        foreach (var thread in threads)
            thread.Start();
        foreach (var thread in threads)
            thread.Join();
        return (resultTree, cost);
    }

    private void ThreadFunction(List<List<Pair<Edge<int>, long>>> adj, List<Pair<Edge<int>, long>> list,
        List<Edge<int>> resultTree, int n, int threadId, Barrier inBar, Barrier outBar, Barrier midBar,
        ref long cost, ref bool isEdge, ref bool succeed) 
    {
        while(true)
        {
            inBar.SignalAndWait();
            for (var k = threadId; k < n; k += _threadNum)
                ConsiderVertex(adj, list, k, ref isEdge);
            
            outBar.SignalAndWait();
            if(succeed)
                break;
            
            for (var k = threadId; k < n; k += _threadNum)
            {
                if (_findUnion.Find(k) != k) 
                    continue;
                
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
            
            midBar.SignalAndWait();
            
            for (var k = threadId; k < n; k += _threadNum) 
            {
                if(list[k] is null)
                    continue;
                
                MergeComponents(resultTree, list[k], ref cost);
                list[k] = null;
            }
        }
    }

    private void ConsiderVertex(List<List<Pair<Edge<int>, long>>> adj, List<Pair<Edge<int>, long>> list,
        int k, ref bool isEdge) 
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

    private void MergeComponents(List<Edge<int>> resultTree, Pair<Edge<int>, long> x, ref long cost) 
    {
        lock(_findUnion) 
        {
            var firstIdx = _findUnion.Find(x.First.First);
            var secIdx = _findUnion.Find(x.First.Second);
            if(firstIdx == secIdx)
                return;
            _findUnion.Union(x.First.First, x.First.Second);
            resultTree.Add(x.First);
            cost += x.Second;
        }
    }
    
}