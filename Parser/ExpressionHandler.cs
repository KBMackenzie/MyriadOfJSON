using System;
using System.Linq;
using System.Collections.Generic;
using MiscellaneousJSON.Helpers;
using UnityEngine;
using DiskCardGame;
using NCalc;

namespace MiscellaneousJSON.Parser;

public static class ExpressionHandler
{
    public static Expression? CardPredicate(string? str, CardInfo card)
    {
        if (str == null || str.IsWhiteSpace()) return null;

        /*
        // Add all list params!
        str = str.ReplaceListParameter(ParamNames.Tribes, card.tribes)
            .ReplaceListParameter(ParamNames.Traits, card.traits)
            .ReplaceListParameter(ParamNames.GemsCost, card.GemsCost)
            .ReplaceListParameter(ParamNames.Abilities, card.Abilities)
            .ReplaceListParameter(ParamNames.SpecialAbilities, card.SpecialAbilities)
            .ReplaceListParameter(ParamNames.MetaCategories, card.metaCategories);

        Plugin.LogInfo($"Final string: {str}");
        */

        // Expression is a predicate to filter the cards with.
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
