using System.Text.RegularExpressions;
using DiskCardGame;
using MiscellaneousJSON.Helpers;

namespace MiscellaneousJSON.Parser.Functions;
using WorldFunc = System.Func<string, MiscellaneousJSON.Parser.NCalcBool>;

public static class AsWorldPredicate 
{
    public static readonly Regex FuncRegex = FunctionRegex.Generate(CardPredicates.Names); 

    public static string ParseFunctions(string exp)
    {
        /*
        if (!FuncRegex.IsMatch(exp)) return exp; 
        string[] groups = RegexHelpers.FirstMatch(FuncRegex, exp);
        string replace = Interpret(card, groups[3], groups[4]);
        return ParseFunctions(card, groups[1]) + replace + groups[5];
        */
        throw new System.NotImplementedException();
    }

    public static string Interpret(string func, string funcParam)
    {
        /*
        CardFunc fn = CardPredicates.Functions[func];
        return fn(card, funcParam.TrimSingleQuotes()); 
        */
        throw new System.NotImplementedException();
    }
}
