using System.Text.RegularExpressions;
using MyriadOfJSON.Helpers;

namespace MyriadOfJSON.Parser.Functions;

public static class FunctionRegex
{
    public static Regex Generate(string[] functionNames) => new (
            @"^(.*)(("
            + functionNames.AsRegexOr() 
            + @")\((.*)\))(.*)$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase
        ); 

    /* FunctionRegex Capture Groups:
     * [0] - full expression, ignore
     * [1] - part before func
     * [2] - full func body
     * [3] - func name
     * [4] - func params
     * [5] - part after func
     */
}
