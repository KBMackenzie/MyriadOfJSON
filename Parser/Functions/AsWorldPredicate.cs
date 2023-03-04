using System.Text.RegularExpressions;
using DiskCardGame;
using MyriadOfJSON.Helpers;

namespace MyriadOfJSON.Parser.Functions;
using WorldFunc = System.Func<string, MyriadOfJSON.Parser.NCalcBool>;

public static class AsWorldPredicate 
{
    public static readonly Regex FuncRegex = FunctionRegex.Generate(WorldPredicates.Names); 

    public static string ParseAllFunctions(string exp)
    {
        if (!FuncRegex.IsMatch(exp)) return exp; 
        string[] groups = RegexHelpers.FirstMatch(FuncRegex, exp);
        string replace = Interpret(groups[3], groups[4]);
        return ParseAllFunctions(groups[1]) + replace + groups[5];
    }

    public static string Interpret(string func, string funcParam)
    {
        WorldFunc fn = WorldPredicates.Functions[func];
        return fn(funcParam.TrimSingleQuotes());
    }
}
