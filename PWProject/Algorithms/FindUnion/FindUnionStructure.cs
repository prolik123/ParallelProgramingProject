 
namespace Algorithms.FindUnion;

// find union in O(n*a(n)) time where a(n) is inverse ackermann function
public class FindUnionStructure : IFindUnion // it can be generic but will be much slower in this case
{
    private Component[] _list;

    public FindUnionStructure(int n) => _list = Enumerable.Range(0, n)
        .Select(x => new Component(x)).ToArray();

    public int Find(int x) => FindInternal(x).Element;

    public void Union(int x, int y) => FindInternal(x).MergeWith(FindInternal(y));

    private Component FindInternal(int x) 
    {
        var current = _list[x];
        if(current.Element == x)
            return current;
        var result = FindInternal(current.Element);
        current.Element = result.Element;
        return result;
    }   

    private class Component 
    {
        public int Element { get; set; }
        private int _rank { get; set; }

        public Component(int element) 
        {
            Element = element;
            _rank = 1;
        }

        public void MergeWith(Component component) 
        {
            if(Element == component.Element)
                return;
            if(_rank > component._rank)
                component.Element = Element;
            else if(_rank < component._rank)
                Element = component.Element;
            else 
            {
                _rank++;
                component._rank++;
            }
        }
    }
}