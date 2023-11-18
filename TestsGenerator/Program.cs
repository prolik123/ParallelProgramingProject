
// 1 = fileName, 2 - minN, 3 - maxN, 4 - minM, 5 - maxM, 6 - maxCost
var rand = new Random();
using (StreamWriter writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, args[1])))
{
    var genResult = new TestCasesGenerator().Generate(int.Parse(args[2]),
        int.Parse(args[3]), int.Parse(args[4]), int.Parse(args[5]), int.Parse(args[6]));
    writer.WriteLine($"{genResult.n} {genResult.m}");
    foreach(var list in genResult.edges) {
        foreach(var edge in list) {
            if(edge.u > edge.v)
                continue;
            if(rand.Next(0,2) == 0)
                writer.WriteLine($"{edge.u} {edge.v} {edge.cost}");
            else
                writer.WriteLine($"{edge.v} {edge.u} {edge.cost}");
        }
    }
}