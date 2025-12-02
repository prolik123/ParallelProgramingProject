namespace TestsGenerator;

internal class TestCasesGenerator 
{
    public (int n, int m, List<List<(int u, int v, int cost)>> edges) Generate(int minN, int maxN, int minM, int maxM, int maxCost) 
    {
        var randomEngine = new Random();
        var n = randomEngine.Next(minN, maxN + 1);
        var m = randomEngine.Next(int.Max(minM, n-1), int.Max(n-1 + 1,maxM + 1));
        
        var dict = new Dictionary<int, List<(int u, int v, int cost)>>();
        for (var k = 1; k < n; k++) 
        {
            var x = randomEngine.Next(0, k);
            var cost = randomEngine.Next(0, maxCost + 1);
            AddToListInDict(dict, k, (k, x, cost));
            AddToListInDict(dict, x, (x, k, cost));
        }

        for (var k = 0; k < m - n + 1; k++) 
        {
            var u = randomEngine.Next(0, n);
            var v = randomEngine.Next(0, n);
            if( u == v || dict[u].Any(x => x.v == v))
            {
                k--;
                continue;
            }
            
            var cost = randomEngine.Next(0, maxCost+1);
            AddToListInDict(dict, u, (u, v, cost));
            AddToListInDict(dict, v, (v, u, cost));
        }

        return (n, m, dict.OrderBy(x => x.Key).Select(x => x.Value).ToList());
    }

    private static void AddToListInDict(IDictionary<int, List<(int u, int v, int cost)>> dict, int x, (int u, int v, int cost) value) 
    {
        if(dict.TryGetValue(x, out _))
            dict[x].Add(value);
        else
            dict.Add(x, [value]);
    }
}