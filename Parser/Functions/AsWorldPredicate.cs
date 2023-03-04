using System.Text.RegularExpressions;
using DiskCardGame;
using MiscellaneousJSON.Helpers;

namespace MiscellaneousJSON.Parser.Functions;
using WorldFunc = System.Func<string, MiscellaneousJSON.Parser.NCalcBool>;

public static class AsWorldPredicate 
{
    public static readonly Regex FuncRegex = FunctionRegex.Generate(WorldPredicates.Names); 

    public static string ParseFunctions(string exp)
    {
        if (!FuncRegex.IsMatch(exp)) return exp; 
        string[] groups = RegexHelpers.FirstMatch(FuncRegex, exp);
        string replace = Interpret(groups[3], groups[4]);
        return ParseFunctions(groups[1]) + replace + groups[5];
    }

    public static string Interpret(string func, string funcParam)
    {
        WorldFunc fn = WorldPredicates.Functions[func];
        return fn(funcParam.TrimSingleQuotes());
    }
}
