using System;
using System.Text;

namespace MiscellaneousJSON.Helpers;

public static class ArrayExtensions
{
    public static void ForEach<T>(this T[] arr, Action<T> fn)
    {
        foreach (T item in arr) fn(item);
    }

    public static string AsRegexOr(this string[] arr)
    {
        StringBuilder sb = new();
        for(int i = 0; i < arr.Length; i++)
        {
            sb.Append(arr[i]);
            if (i < arr.Length - 1) sb.Append('|');
        }
        return sb.ToString();
    }

}
