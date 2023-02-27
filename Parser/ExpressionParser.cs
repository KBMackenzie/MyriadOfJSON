using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using DiskCardGame;

namespace MiscellaneousJSON.Parser;

public static class ExpressionParser
{
    public static string[][] MatchGroups(Regex reg, string str)
       => reg.Matches(str).Cast<Match>().Select(GetGroups).ToArray();

    public static string[] FirstMatch(Regex reg, string str)
        => MatchGroups(reg, str)[0];

    public static string[] GetGroups(Match match)
        => match.Groups.Cast<Group>().Select(x => x.Value).ToArray();

    // Parses all functions in an expression
    public static string ParseFunctions(CardInfo card, string exp)
    {
        Regex reg = FunctionInterpreter.FunctionRegex;
        if (!reg.IsMatch(exp)) return exp; 
        string[] groups = FirstMatch(reg, exp);
        string replace = ""; // EvaluateFunction(group[3], group[4]); 
        return ParseFunctions(card, groups[1]) + replace + groups[5];
    }
}
