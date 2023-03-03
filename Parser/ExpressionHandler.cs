﻿using System;
using NCalc;
using MiscellaneousJSON.Helpers;
using MiscellaneousJSON.Parser.Functions;
using MiscellaneousJSON.Parser.Names;
using DiskCardGame;

#pragma warning disable Publicizer001
namespace MiscellaneousJSON.Parser;

public static class ExpressionHandler
{
    public static Expression? CardPredicate(string? str, CardInfo card)
    {
        // Expression is a predicate to filter the cards with.
        if (str == null || str.IsWhiteSpace()) return null;

        // Parse all functions
        str = AsCardPredicate.ParseAllFunctions(card, str);

        Plugin.LogInfo($"Parsed all functions: {str}");
        // Create expression.
        Expression pred = new Expression(str);

        // Additional params!
        pred.Parameters[VarNames.BloodCost] = card.BloodCost;
        pred.Parameters[VarNames.BoneCost] = card.BonesCost;
        pred.Parameters[VarNames.EnergyCost] = card.EnergyCost;
        pred.Parameters[VarNames.Temple] = card.temple.ToString();

        pred.Parameters[VarNames.Name] = card.name;
        pred.Parameters[VarNames.DisplayedName] = card.displayedName;

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