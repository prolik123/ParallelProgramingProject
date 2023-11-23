
using DataStructures;
using Algorithms.FindUnion;
using System.Diagnostics;

namespace Algorithms.MST;

public class ThreadBoruvkaMST : IMST
{
    private readonly int THREAD_NUM;
    private StarFindUnion _findUnion;
    private List<List<Pair<Edge<int>, long>>> _adj;
    private List<Pair<Edge<int>, long>> _list;

    private List<Edge<int>> resultTree;
    private int n;
    private bool success;
    private bool isEdge;
    private long cost;

    public ThreadBoruvkaMST(List<List<Pair<Edge<int>, long>>> adj, int n, int threadNum) 
    {
        _adj = new (adj);
        this.n = n;
        _findUnion = new StarFindUnion(n);
        THREAD_NUM = threadNum;
    }

    public (IEnumerable<Edge<int>> edges, long cost) GetMST()
    {
        if(n < 2)
            return (Enumerable.Empty<Edge<int>>(), 0);
        success = false;
        resultTree = new List<Edge<int>>();
        cost = 0 ;
        _list = new List<Pair<Edge<int>, long>>();
        for(int k=0;k<n;k++)
            _list.Add(new Pair<Edge<int>, long>(new Edge<int>(-1,-1),-1));

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        Thread[] threads = new Thread[THREAD_NUM];
        Barrier inBarrier = new Barrier(THREAD_NUM, (e) => isEdge = false);
        Barrier outBarrier = new Barrier(THREAD_NUM, (b) => success = !isEdge);
        Barrier midBarrier = new Barrier(THREAD_NUM);
        for(int k=0;k<THREAD_NUM;k++) 
        {
            int specInt = k;
            threads[k] = new Thread(() => ThreadFunction(specInt, inBarrier, outBarrier, midBarrier));
        }
        foreach (var thread in threads)
            thread.Start();
        foreach (var thread in threads)
            thread.Join();
        stopwatch.Stop();
        return (resultTree, cost);
    }

    private void ThreadFunction(int threadId, Barrier inBar, Barrier outBar, Barrier midBar) 
    {
        while(true)
        {
            inBar.SignalAndWait();
            for(int k = threadId; k<n;k+=THREAD_NUM)
                ConsiderVertex(k);
            outBar.SignalAndWait();
            if(success)
                break;
            for(int k=threadId;k<n;k+=THREAD_NUM) 
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
            midBar.SignalAndWait();
            for(int k=threadId;k<n;k+=THREAD_NUM) 
            {
                if(_list[k] is null)
                    continue;
                MergeComponents(_list[k]);
                _list[k] = null;
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

    private void MergeComponents(Pair<Edge<int>, long> x) 
    {
        // Tu jest problem z tym lockiem ale nie moge znalesc zadnego opisu bez compare exchange albo locka
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