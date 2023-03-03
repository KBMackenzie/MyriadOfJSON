using System.Text.RegularExpressions;
using DiskCardGame;
using MiscellaneousJSON.Helpers;

namespace MiscellaneousJSON.Parser.Functions;
using CardAction = PlayableCardAction<string>; 

public static class AsAction 
{
    public static readonly Regex FuncRegex = FunctionRegex.Generate(CardActions.Names); 

    public static void ParseFunctions(ref PlayableCard card, string[] exps)
    {
        foreach (string exp in exps)
        {
            if(!FuncRegex.IsMatch(exp))
            {
                Plugin.LogError($"Invalid card action function: {exp}");
                continue;
            }
            string[] groups = RegexHelpers.FirstMatch(FuncRegex, exp);
            Interpret(ref card, groups[3], groups[4]);
        }
    }

    public static void Interpret(ref PlayableCard card, string func, string funcParam)
    {
        CardAction fn = CardActions.Functions[func];
        fn(ref card, funcParam.TrimSingleQuotes());
    }
}
