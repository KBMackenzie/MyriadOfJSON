using System.Collections.Generic;

public static class ListExtensions
{
    public static T? SafelyGet<T>(this List<T> list, int index)
    {
        if (index >= list.Count) return default(T);
        return list[index];
    }
}
