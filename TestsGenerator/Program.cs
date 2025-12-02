using TestsGenerator;

var rand = new Random();
var fileName = args[1];
var minN = int.Parse(args[2]);
var maxN = int.Parse(args[3]);
var minM = int.Parse(args[4]);
var maxM = int.Parse(args[5]);
var maxCost = int.Parse(args[6]);

using var writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, fileName));

var genResult = new TestCasesGenerator().Generate(minN, maxN, minM, maxM, maxCost);
writer.WriteLine($"{genResult.n} {genResult.m}");
foreach (var edge in genResult.edges.SelectMany(list => list.Where(edge => edge.u <= edge.v)))
{
    writer.WriteLine(rand.Next(0, 2) == 0 ? $"{edge.u} {edge.v} {edge.cost}" : $"{edge.v} {edge.u} {edge.cost}");
}