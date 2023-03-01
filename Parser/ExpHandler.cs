using System;
using NCalc;
using MiscellaneousJSON.Helpers;
using DiskCardGame;

namespace MiscellaneousJSON.Parser;

public static class ExpHandler
{
    public static Expression? CardPredicate(string? str, CardInfo card)
    {
        // Expression is a predicate to filter the cards with.
        if (str == null || str.IsWhiteSpace()) return null;

        // Parse all functions
        str = FuncParser.ParseFunctions(card, str);

        // Create expression.
        Expression pred = new Expression(str);

        // Additional params!
        pred.Parameters[ParamNames.BloodCost] = card.BloodCost;
        pred.Parameters[ParamNames.BoneCost] = card.BonesCost;
        pred.Parameters[ParamNames.EnergyCost] = card.EnergyCost;
        pred.Parameters[ParamNames.Temple] = card.temple.ToString();

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
