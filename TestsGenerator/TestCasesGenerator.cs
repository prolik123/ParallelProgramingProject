
public class TestCasesGenerator 
{
    public (int n, int m, List<List<(int u, int v, int cost)>> edges) Generate(int minN, int maxN, int minM, int maxM, int maxCost) 
    {
        var randomEngine = new Random();
        int n = randomEngine.Next(minN, maxN + 1);
        int m = randomEngine.Next(int.Max(minM, n-1), int.Max(n-1 + 1,int.Min(maxM + 1,n*(n-1)/2 )));
        var dict = new Dictionary<int, List<(int u, int v, int cost)>>();
        for(int k=1;k<n;k++) 
        {
            int x = randomEngine.Next(0, k);
            int cost = randomEngine.Next(0, maxCost + 1);
            AddToListInDict(dict, k, (k, x, cost));
            AddToListInDict(dict, x, (x, k, cost));
        }

        for(int k=0;k<m-n;k++) {
            int u = randomEngine.Next(0, n);
            int v = randomEngine.Next(0, n);
            if(dict[u].Any(x => x.v == v))
            {
                k--;
                continue;
            }
            int cost = randomEngine.Next(0, maxCost+1);
            AddToListInDict(dict, u, (u, v, cost));
            AddToListInDict(dict, v, (v, u, cost));
        }

        return (n, m, dict.OrderBy(x => x.Key).Select(x => x.Value).ToList());
    }

    private void AddToListInDict(IDictionary<int, List<(int u, int v, int cost)>> dict, int x, (int u, int v, int cost) value) 
    {
        if(dict.TryGetValue(x, out _))
            dict[x].Add(value);
        else
            dict.Add(x, new List<(int u, int v, int cost)>(){value});
    }
}