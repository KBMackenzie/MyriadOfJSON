using System.Text.RegularExpressions;
using DiskCardGame;
using MyriadOfJSON.Helpers;

namespace MyriadOfJSON.Parser.Functions;
using CardAction = System.Action<DiskCardGame.PlayableCard, string>;

public static class AsCardAction 
{
    public static readonly Regex FuncRegex = FunctionRegex.Generate(CardActions.Names); 

    /* these functions are meant to work like ~callbacks~ and they're applied to the card in order! <3 */
    public static void ParseAllFunctions(PlayableCard card, string[] exps)
    {
        foreach (string exp in exps)
        {
            if(!FuncRegex.IsMatch(exp))
            {
                Plugin.LogError($"Invalid card action function: {exp}");
                continue;
            }
            string[] groups = RegexHelpers.FirstMatch(FuncRegex, exp);
            Interpret(card, groups[3], groups[4]);
        }
    }

    public static void Interpret(PlayableCard card, string func, string funcParam)
    {
        CardAction fn = CardActions.Functions[func.ToLower()];
        fn(card, funcParam.TrimSingleQuotes());
    }
}
