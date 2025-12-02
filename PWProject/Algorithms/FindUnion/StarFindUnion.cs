
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PWProject.Algorithms.FindUnion;

public class StarFindUnion : IFindUnion
{
    private readonly StarComponent[] _list;

    public StarFindUnion(int n) => _list = Enumerable.Range(0, n).Select(x => new StarComponent(x)).ToArray();

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
        foreach(var item in _list[x]) 
        {
            result.Add(item);
            GetMembers(item, result);
        }
    }

    private StarComponent FindInternal(int x)
    {
        while (true)
        {
            var current = _list[x];
            if (current.Element == x) 
                return current;
            x = current.Element;
        }
    }

    private class StarComponent : IEnumerable<int>
    {
        private int _rank;
        private readonly List<int> _list;

        public StarComponent(int element) 
        {
            Element = element;
            _rank = 1;
            _list = [];
        }
        
        public int Element { get; private set; }

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

        public IEnumerator<int> GetEnumerator() => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}