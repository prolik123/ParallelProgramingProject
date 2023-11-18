using Algorithms.FindUnion;
using DataStructures;

namespace Algorithms.MST;

public class BoruvkaMST : IMST
{
    private IFindUnion _findUnion;
    private List<List<Pair<Edge<int>, long>>> _adj;

    public (IEnumerable<Edge<int>> edges, long cost) GetMST(List<List<Pair<Edge<int>, long>>> adj, int n, int m)
    {
        if(n < 2)
            return (Enumerable.Empty<Edge<int>>(), 0);
        _findUnion = new FindUnionStructure(n);
        _adj = adj;
        var resultTree = new List<Edge<int>>();
        long cost = 0 ;
        bool isSingleComponent;
        do
        {
            Dictionary<int, Pair<Edge<int>, long>> dict = new Dictionary<int, Pair<Edge<int>, long>>();
            for(int k=0;k<n;k++) 
                AddOrUpdate(dict, _findUnion.Find(k), FindMinimalEdgeForVertex(k));
            foreach(var x in dict) 
            {
                var firstIdx = _findUnion.Find(x.Value.First.First);
                var secIdx = _findUnion.Find(x.Value.First.Second);
                if(firstIdx == secIdx)
                    continue;
                _findUnion.Union(x.Value.First.First, x.Value.First.Second);
                resultTree.Add(x.Value.First);
                cost += x.Value.Second;
            }

            isSingleComponent = dict.Count < 2;
        }while(isSingleComponent);

        return (resultTree, cost);
    }

    private Pair<Edge<int>, long> FindMinimalEdgeForVertex(int x) 
    {
        var conectedNumber = _findUnion.Find(x); // for speed
        // Where() here do not slow it down
        return FindMinimalEdge(_adj[conectedNumber].Where(x => _findUnion.Find(x.First.Second) != conectedNumber));
    }

    private Pair<Edge<int>, long> FindMinimalEdge(IEnumerable<Pair<Edge<int>, long>> edges) 
        => edges.Aggregate((prev, next) => prev.Second > next.Second ? next : prev);

    private void AddOrUpdate(IDictionary<int, Pair<Edge<int>, long>> dict, int idx, Pair<Edge<int>, long> value)
    {
        if(dict.TryGetValue(idx, out _))
            dict[idx] = value;
        else
            dict.Add(idx, value);
    }
}