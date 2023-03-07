using System;
using NCalc;
using MyriadOfJSON.Helpers;
using MyriadOfJSON.Parser.Functions;
using MyriadOfJSON.Parser.Names;
using MyriadOfJSON.Parser.Variables;
using DiskCardGame;

#pragma warning disable Publicizer001
namespace MyriadOfJSON.Parser;

public static class ExpressionHandler
{
    public static Expression? CardPredicate(string? str, CardInfo card, bool world = true)
    {
        if (str == null || str.IsWhiteSpace()) return null;

        /* parse all functions! */
        str = AsCardPredicate.ParseAllFunctions(card, str);
        if (world) str = AsWorldPredicate.ParseAllFunctions(str);

        Plugin.LogInfo($"Parsed all functions: {str}");

        Expression pred = new Expression(str);
        /* add card variables! */
        MakeVariables.CardVariables(pred, card);
        /* add world variables! c: */
        if (world) MakeVariables.WorldVariables(pred);

        return pred;
    }

    public static Expression? WorldPredicate(string str)
    {
        // Expression is a predicate to filter the cards with.
        if (str == null || str.IsWhiteSpace()) return null;

        str = AsWorldPredicate.ParseAllFunctions(str);

        Plugin.LogInfo($"Parsed all functions: {str}");

        Expression pred = new Expression(str); 
        // Add world variables
        MakeVariables.WorldVariables(pred);

        return pred;
    }

    public static bool SafeEvaluation(Expression? predicate)
    {
        object? result;
        try
        {
            result = predicate?.Evaluate();
        }
        catch (Exception e)
        {
            Plugin.LogError($"Invalid expression: {predicate?.ToString() ?? "(null)"}");
            Plugin.LogError(e.Message);
            return true; // Default to 'true'.
        }

        return result is bool b && b;
    }

    public static int? SafelyParseAsInt(Expression? expression)
    {
        object? result;
        try
        {
            result = expression?.Evaluate();
        }
        catch (Exception e)
        {
            Plugin.LogError($"Invalid expression: {expression?.ToString() ?? "(null)"}");
            Plugin.LogError(e.Message);
            return null;
        }

        if (result == null || result is not int)
        {
            Plugin.LogError($"Invalid expression: Expression doesn't evaluate to an integer!");
            return null;
        }

        return (int) result;
    }
}
