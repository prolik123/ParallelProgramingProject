
using DataStructures;

namespace Algorithms.MST;

public class MstTest : IMST
{
    public (IEnumerable<Edge<int>> edges, long cost) GetMST(List<List<Pair<Edge<int>, long>>> adj, int n, int m)
    {
        var visited = new bool[n];
        for(int k=0;k<n;k++)
            visited[k] = false;
        var queue = new PriorityQueue<Edge<int>, long>();
        adj[0].ForEach(x => queue.Enqueue(x.First, x.Second));
        visited[0] = true;
        long cost = 0;
        int numOfVisited = 1;
        while(numOfVisited != n) {
            var element = queue.Dequeue();
            if(visited[element.Second])
                continue;
            numOfVisited++;
            visited[element.Second] = true;
            cost += adj[element.First].Find(x => x.First.Second == element.Second).Second;
            adj[element.Second].ForEach(x => queue.Enqueue(x.First, x.Second));
        }
        return (null, cost);
    }
}