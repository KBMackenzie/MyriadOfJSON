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
        // Stringify lists for usage (add support for user-made ones later. :>) 
        // I can do so by replacing the .ToString() with another method.
        // That method should look for the actual ability/trait/tribe name and return that!
        string tribeList = card.tribes.Select(x => x.ToString()).StringifyList();
        string traitList = card.traits.Select(x => x.ToString()).StringifyList();
        string gemCostList = card.GemsCost.Select(x => x.ToString()).StringifyList();
        string abilityList = card.Abilities.Select(x => x.ToString()).StringifyList();
        string specialAbilities = card.SpecialAbilities.Select(x => x.ToString()).StringifyList();

        // Actually replace things in the string before making the expression!
        str = str.Replace("[Tribes]", tribeList)
            .Replace("[Traits]", traitList)
            .Replace("[GemsCost]", gemCostList)
            .Replace("[Abilities]", abilityList)
            .Replace("[SpecialAbilities]", specialAbilities);
        */

        // Add all list params!
        str = str.ReplaceListParameter("[Tribes]", card.tribes)
            .ReplaceListParameter("[Traits]", card.traits)
            .ReplaceListParameter("[GemCost]", card.GemsCost)
            .ReplaceListParameter("[Abilities]", card.Abilities)
            .ReplaceListParameter("[SpecialAbilities]", card.SpecialAbilities)
            .ReplaceListParameter("[MetaCategories]", card.metaCategories);

        Plugin.LogInfo($"Final string: {str}");

        // Expression is a predicate to filter the cards with.
        Expression pred = new Expression(str);

        // Parameters!
        // 1. Costs
        pred.Parameters["BloodCost"] = card.BloodCost;
        pred.Parameters["BoneCost"] = card.BonesCost;
        pred.Parameters["EnergyCost"] = card.EnergyCost;
        pred.Parameters["Temple"] = card.temple.ToString();

        return pred;
    }

    public static bool SafeEvaluation(Expression? predicate)
    {
        object? result;
        try
        {
            result = predicate?.Evaluate();
        }
        catch (Exception)
        {
            Plugin.LogError($"Invalid expression: {predicate?.Error ?? "(null)"}");
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
        catch (Exception)
        {
            Plugin.LogError($"Invalid expression: {expression?.Error ?? "(null)"}");
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
