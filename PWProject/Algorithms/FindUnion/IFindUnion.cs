namespace Algorithms.FindUnion;

public interface IFindUnion // it can be generic but it will be much slower
{
    public int Find(int x);
    public void Union(int x, int y);
}