using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using DiskCardGame;
using System.Text;
using MiscellaneousJSON.Helpers;

namespace MiscellaneousJSON.Parser;

// Type alias
using CardFunc = System.Func<DiskCardGame.CardInfo, string, MiscellaneousJSON.Parser.NCalcBool>;

public static class FuncParser
{
    public static readonly Regex FunctionRegex = new (
            @"^(.*)(("
            + FunctionLib.AllNames.AsRegexOr() 
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
    
    public static string ParseFunctions(CardInfo card, string exp)
    {
        if (!FunctionRegex.IsMatch(exp)) return exp; 
        string[] groups = RegexHelpers.FirstMatch(FunctionRegex, exp);
        string replace = Interpret(card, groups[3], groups[4]);
        return ParseFunctions(card, groups[1]) + replace + groups[5];
    }

    public static string Interpret(CardInfo card, string func, string funcParam)
    {
        CardFunc fn = FunctionLib.Functions[func];
        return fn(card, funcParam.TrimSingleQuotes()); 
    }
}
