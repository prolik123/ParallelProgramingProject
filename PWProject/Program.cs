using System.Diagnostics;
using PWProject.Algorithms.MST;
using PWProject.DataStructures;

var firstLine = Console.ReadLine()?.Split() ?? throw new Exception("Invalid program input");
var n = int.Parse(firstLine[0]);
var m = int.Parse(firstLine[1]);
var edges = new List<List<Pair<Edge<int>, long>>>(n);

for (var k = 0; k < m; k++)
{
    const string invalidInputMessage = "Invalid program input, expect m lines with properly described edges";
    var line = Console.ReadLine()?.Split() ?? throw new Exception(invalidInputMessage);
    
    var u = int.Parse(line[0]);
    var v = int.Parse(line[1]);
    var cost = long.Parse(line[2]);
    edges[u].Add(new Pair<Edge<int>,long>(new Edge<int>(u,v), cost));
    edges[v].Add(new Pair<Edge<int>,long>(new Edge<int>(v,u), cost));
}


const int numOfTries = 5;

for (var k = 0; k < numOfTries; k++)
{
    IMST mstSolver;
    if (args.Length <= 1)
        mstSolver = new BoruvkaMST(n);
    else
    {
        mstSolver = args[1] switch
        {
            "parallel" => new ParallelBoruvkaMST(n, 1 << k),
            "test" => new MstTest(),
            "threads" => new ThreadBoruvkaMST(n, 1 << k),
            "fast" => new FastBoruvkaMST(n, 1 << k),
            _ => new BoruvkaMST(n)
        };
    }

    ThreadPool.SetMaxThreads(1 << k, 1 << k);
    ThreadPool.SetMinThreads(1 << k, 1 << k);
    var stopwatch = new Stopwatch();

    stopwatch.Start();
    var result = mstSolver.GetMST(edges, n);
    stopwatch.Stop();
    Console.Error.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms for {1 << k} threads");
    Console.WriteLine(result.cost);
}
