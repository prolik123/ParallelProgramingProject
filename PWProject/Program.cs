using System.Diagnostics;
using Algorithms.MST;
using DataStructures;

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

IMST mstSolver;

if(args.Length > 1) 
{
    mstSolver = args[1] switch 
    {
        "parallel" => new ParallelBoruvkaMST(edges, n),
        "test" => new MstTest(edges, n),
        "threads" => new ThreadBoruvkaMST(edges, n),
        "fast" => new FastBoruvkaMST(edges, n),
        _ =>  new BoruvkaMST(edges, n)
    };
}
else
    mstSolver = new BoruvkaMST(edges, n);
var stopwatch = new Stopwatch();

stopwatch.Start();
var result = mstSolver.GetMST();
stopwatch.Stop();
Console.Error.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms");

Console.WriteLine(result.cost);

/*
foreach(var edge in result.edges) 
{
    Console.WriteLine($"{edge.First} {edge.Second}");
}
*/