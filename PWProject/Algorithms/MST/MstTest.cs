
using System.Collections.Generic;
using PWProject.DataStructures;

namespace PWProject.Algorithms.MST;

public class MstTest : IMST
{

    public (IEnumerable<Edge<int>> edges, long cost) GetMST(List<List<Pair<Edge<int>, long>>> adjParam, int n)
    {
        var adj = new List<List<Pair<Edge<int>, long>>>(adjParam);
        var visited = new bool[n];
        for (var k = 0; k < n; k++)
            visited[k] = false;
        var queue = new PriorityQueue<Edge<int>, long>();
        
        adj[0].ForEach(x => queue.Enqueue(x.First, x.Second));
        visited[0] = true;
        long cost = 0;
        var result = new List<Edge<int>>();
        var numOfVisited = 1;
        
        while(numOfVisited != n) 
        {
            var element = queue.Dequeue();
            if(visited[element.Second])
                continue;
            
            result.Add(element);
            numOfVisited++;
            visited[element.Second] = true;
            cost += adj[element.First].Find(x => x.First.Second == element.Second).Second;
            adj[element.Second].ForEach(x => queue.Enqueue(x.First, x.Second));
        }
        
        return (result, cost);
    }
}