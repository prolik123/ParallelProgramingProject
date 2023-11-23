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
var ok = ThreadPool.SetMinThreads(1, 1);

for(int k=0;k<5;k++) {
    IMST mstSolver;

    if(args.Length > 1) 
    {
        mstSolver = args[1] switch 
        {
            "parallel" => new ParallelBoruvkaMST(edges, n),
            "test" => new MstTest(edges, n),
            "threads" => new ThreadBoruvkaMST(edges, n, 1 << k),
            "fast" => new FastBoruvkaMST(edges, n),
            _ =>  new BoruvkaMST(edges, n)
        };
    }
    else
        mstSolver = new BoruvkaMST(edges, n);
    var maxOk =ThreadPool.SetMaxThreads(1 << k, 1 << k);
    var minOk = ThreadPool.SetMinThreads(1 << k, 1 << k);
    var stopwatch = new Stopwatch();

    stopwatch.Start();
    var result = mstSolver.GetMST();
    stopwatch.Stop();
    Console.Error.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms for {1 << k} threads");
    Console.WriteLine(result.cost);
}


/*
foreach(var edge in result.edges) 
{
    Console.WriteLine($"{edge.First} {edge.Second}");
}
*/