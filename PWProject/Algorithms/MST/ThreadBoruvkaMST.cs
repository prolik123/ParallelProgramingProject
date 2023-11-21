
using DataStructures;
using Algorithms.FindUnion;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Algorithms.MST;

public class ThreadBoruvkaMST : IMST
{
    private const int THREAD_NUM = 16;
    private IFindUnion _findUnion;
    private List<List<Pair<Edge<int>, long>>> _adj;
    private List<Pair<Edge<int>, long>> _list;

    private ConcurrentBag<Edge<int>> resultTree;
    private int n;
    private bool success;
    private bool isEdge;
    private long cost;

    public ThreadBoruvkaMST(List<List<Pair<Edge<int>, long>>> adj, int n) 
    {
        _adj = adj;
        this.n = n;
        _findUnion = new FindUnionStructure(n);
        _adj = adj;
    }

    public (IEnumerable<Edge<int>> edges, long cost) GetMST()
    {
        if(n < 2)
            return (Enumerable.Empty<Edge<int>>(), 0);
        success = false;
        resultTree = new ConcurrentBag<Edge<int>>();
        cost = 0 ;
        _list = new List<Pair<Edge<int>, long>>();
        for(int k=0;k<n;k++)
            _list.Add(new Pair<Edge<int>, long>(new Edge<int>(-1,-1),-1));

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        Thread[] threads = new Thread[THREAD_NUM];
        Barrier inBarrier = new Barrier(THREAD_NUM, (e) => isEdge = false);
        Barrier outBarrier = new Barrier(THREAD_NUM, (b) => success = !isEdge);
        for(int k=0;k<THREAD_NUM;k++) 
        {
            int specInt = k;
            threads[k] = new Thread(() => ThreadFunction(specInt, inBarrier, outBarrier));
        }
        foreach (var thread in threads)
            thread.Start();
        foreach (var thread in threads)
            thread.Join();
        stopwatch.Stop();
        return (resultTree, cost);
    }

    private void ThreadFunction(int threadId, Barrier inBar, Barrier outBar) 
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
                if(_list[k].First.First == -1)
                    continue;
                MergeComponents(_list[k]);
                _list[k] = new Pair<Edge<int>, long>(new Edge<int>(-1,-1),-1);
            }
        }
    }

    private void ConsiderVertex(int k) 
    {
        var edge = FindMinimalEdgeForVertex(k);
        if(edge is null)
            return;
        var conectedNumber = _findUnion.Find(k);
        lock(_list[conectedNumber]) 
        {
            if(_list[conectedNumber].First.First == -1 || _list[conectedNumber].Second > edge.Second)
                _list[conectedNumber] = edge;
        }
    }
    private Pair<Edge<int>, long> FindMinimalEdgeForVertex(int x) 
    {
        var conectedNumber = _findUnion.Find(x); // for speed
        // Where() here do not slow it down
        var edges = _adj[x].Where(x => _findUnion.Find(x.First.Second) != conectedNumber).ToList();
        if(edges is null || !edges.Any())
            return null;
        isEdge = true;
        _adj[x] = edges;
        return FindMinimalEdge(edges);     
    }

    private Pair<Edge<int>, long> FindMinimalEdge(IEnumerable<Pair<Edge<int>, long>> edges) 
        => edges.Aggregate((prev, next) => prev.Second > next.Second ? next : prev);

    private Pair<Edge<int>, long> Swapper(Pair<Edge<int>, long> a, Pair<Edge<int>, long> b) => a.Second > b.Second ? b : a;
    private void MergeComponents(Pair<Edge<int>, long> x) 
    {
        // Tu jest problem z tym lockiem
        lock(_findUnion) 
        {
            var firstIdx = _findUnion.Find(x.First.First);
            var secIdx = _findUnion.Find(x.First.Second);
            if(firstIdx == secIdx)
                return;
            _findUnion.Union(x.First.First, x.First.Second);
        }
        resultTree.Add(x.First);
        Interlocked.Add(ref cost, x.Second);
    }
    
}