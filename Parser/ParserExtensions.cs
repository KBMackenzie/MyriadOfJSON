using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace MiscellaneousJSON.Parser;

public static class ParserExtensions
{
    public static string StringifyList(this IEnumerable<string> list)
    {
        StringBuilder sb = new StringBuilder();
        string[] strs = list.ToArray();
        for (int i = 0; i < strs.Length; i++)
        {
            sb.Append('\'').Append(strs[i]).Append('\'');
            if (i < strs.Length - 1) sb.Append(',');
        }
        return sb.ToString();
    }

    public static string ReplaceListParameter<T>(this string exp, string param, IEnumerable<T> list) where T : Enum
    {
        if (!exp.Contains(param)) return exp;

        string listAsString = list.Select(x => x.ToString()).StringifyList();
        // If there are none, add at least an empty string, since NCalc yells if you have no items in in()
        if (string.IsNullOrWhiteSpace(listAsString)) listAsString = "\'\'";
        return exp.Replace(param, listAsString);
    }

}
