using System.Text.RegularExpressions;
using DiskCardGame;
using MyriadOfJSON.Helpers;

namespace MyriadOfJSON.Parser.Functions;
using CardFunc = System.Func<DiskCardGame.CardInfo, string, MyriadOfJSON.Parser.NCalcBool>;

public static class AsCardPredicate
{
    public static readonly Regex FuncRegex = FunctionRegex.Generate(CardPredicates.Names); 

    public static string ParseAllFunctions(CardInfo card, string exp)
    {
        if (!FuncRegex.IsMatch(exp)) return exp; 
        string[] groups = RegexHelpers.FirstMatch(FuncRegex, exp);
        string replace = Interpret(card, groups[3], groups[4]);
        return ParseAllFunctions(card, groups[1]) + replace + groups[5];
    }

    public static string Interpret(CardInfo card, string func, string funcParam)
    {
        CardFunc fn = CardPredicates.Functions[func.ToLower()];
        return fn(card, funcParam.TrimSingleQuotes()); 
    }
}
