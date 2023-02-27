using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using DiskCardGame;
using System.Text;

namespace MiscellaneousJSON.Parser;

// Type alias
using CardFunc = System.Func<DiskCardGame.CardInfo, string, bool>;

public static class FunctionInterpreter
{
    public static Dictionary<string, CardFunc> Functions = new();
    
    public static readonly string[] FunctionNames =
    {
        "hasTribe",
        "hasTrait",
        "hasAbility",
        "hasSpecialAbility",
        "hasMetaCategory"
    };

    public static readonly Regex FunctionRegex = new (
            @"^(.*)(("
            + ArrayAsString(FunctionNames)
            + @")\((.*)\))(.*)$"
        ); 

    /* FunctionRegex Capture Groups:
     * [0] - full expression, ignore
     * [1] - part before func
     * [2] - full func body
     * [3] - func name
     * [4] - func params
     * [5] - part after func
     */

    private static string ArrayAsString(string[] arr)
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
