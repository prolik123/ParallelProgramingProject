
namespace Algorithms.FindUnion;

public class StarFindUnion : IFindUnion
{
    private StarComponent[] _list;

    public StarFindUnion(int n) => _list = Enumerable.Range(0, n)
        .Select(x => new StarComponent(x)).ToArray();

    public int Find(int x) => FindInternal(x).Element;

    public void Union(int x, int y) => FindInternal(x).MergeWith(FindInternal(y));

    public List<int> GetMembers(int x) 
    {
        var result = new List<int>{x};
        GetMembers(x, result);
        return result;
    }

    private void GetMembers(int x, List<int> result) 
    {
        foreach(var item in _list[x]._list) 
        {
            result.Add(item);
            GetMembers(item, result);
        }
    }

    private StarComponent FindInternal(int x) 
    {
        var current = _list[x];
        if(current.Element == x)
            return current;
        return FindInternal(current.Element);
    }   

    private class StarComponent 
    {
        public int Element { get; set; }
        private int _rank;
        public List<int> _list;

        public StarComponent(int element) 
        {
            Element = element;
            _rank = 1;
            _list = new List<int>();
        }

        public void MergeWith(StarComponent component) 
        {
            if(Element == component.Element)
                return;
            if(_rank > component._rank) 
            {
                _list.Add(component.Element);
                component.Element = Element;
            }
            else if(_rank < component._rank) 
            {
                component._list.Add(Element);
                Element = component.Element;
            }
            else 
            {
                component._list.Add(Element);
                Element = component.Element;
                _rank++;
                component._rank++;
            }
        }

    }
}