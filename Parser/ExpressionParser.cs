using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using DiskCardGame;

public static class ExpressionParser
{
    public static string[][] MatchGroups(Regex reg, string str)
       => reg.Matches(str).Cast<Match>().Select(GetGroups).ToArray();

    public static string[] FirstMatch(Regex reg, string str)
        => MatchGroups(reg, str)[0];

    public static string[] GetGroups(Match match)
        => match.Groups.Cast<Group>().Select(x => x.Value).ToArray();

    public class ParserBlock
    {
        public CardInfo Card;
        public Regex Regex;
        public string Expression;
        public Func<CardInfo, string, bool> Evaluate;

        public ParserBlock(CardInfo card, Regex regex, string expression,
                Func<CardInfo, string, bool> evaluate)
        {
            Card = card;
            Regex = regex;
            Expression = expression;
            Evaluate = evaluate;
        }

        public bool EvaluateFunc()
            => Evaluate(Card, Expression);

        public string EvaluateAsString()
            => EvaluateFunc() ? "'true'" : "'false'";

        public ParserBlock NewFromExpression(string newExp)
            => new(Card, Regex, newExp, Evaluate);
    }

    // This should be done in an ability by ability basis
    public static string ParseFunction(ParserBlock info)
    {
        if (!info.Regex.IsMatch(info.Expression)) return info.Expression;
        string[] groups = FirstMatch(info.Regex, info.Expression);
        string replace = info.EvaluateAsString(); 
        // Create new parser block with a new expression!
        // (Everything else remains the same.)
        ParserBlock newInfo = info.NewFromExpression(groups[1]);
        return ParseFunction(newInfo) + replace + groups[4];
    }
}
