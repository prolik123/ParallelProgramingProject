using Algorithms.MST;
using DataStructures;

IMST mstSolver = args.Length > 1 && args[1] == "parallel" 
    ? new ParallelBoruvkaMST() 
    : new BoruvkaMST();

var firstLine = Console.ReadLine().Split();
int n = int.Parse(firstLine[0]);
int m = int.Parse(firstLine[1]);
var edges = new List<List<Pair<Edge<int>, long>>>();
for(int k=0;k<n;k++)
    edges.Add(new List<Pair<Edge<int>, long>>());

for(int k=0;k<m;k++) 
{
    var line = Console.ReadLine().Split();
    var u = int.Parse(line[0]);
    var v = int.Parse(line[1]);
    var cost = long.Parse(line[2]);
    edges[u].Add(new Pair<Edge<int>,long>(new Edge<int>(u,v), cost));
    edges[v].Add(new Pair<Edge<int>,long>(new Edge<int>(v,u), cost));
}

// TimeStats start
var result = mstSolver.GetMST(edges, n, m);
// TimeStats end

Console.WriteLine(result.cost);
foreach(var edge in result.edges) 
{
    Console.WriteLine($"{edge.First} {edge.Second}");
}
