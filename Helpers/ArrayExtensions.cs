using System;

namespace MiscellaneousJSON.Helpers;

public static class ArrayExtensions
{
    public static void ForEach<T>(this T[] arr, Action<T> fn)
    {
        foreach (T item in arr) fn(item);
    }
}
