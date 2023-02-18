using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MiscellaneousJSON.Helpers;
using UnityEngine;
using DiskCardGame;
using NCalc;

namespace MiscellaneousJSON.Items.Parser;
public static class ExpressionHandler
{
    public static Expression? CardPredicate(string? str, CardInfo card)
    {
        if (str == null || str.IsWhiteSpace()) return null;

        // Expression is a predicate to filter the cards with.
        Expression pred = new Expression(str);

        // Parameters!
        // 1. Costs
        pred.Parameters["BloodCost"] = card.BloodCost;
        pred.Parameters["BonesCost"] = card.BonesCost;
        pred.Parameters["EnergyCost"] = card.EnergyCost;
        pred.Parameters["GemsCost"] = card.GemsCost.Select(x => x.ToString()); // IEnumerable<string> !! 
        // 2. Temple, Tribes, Traits
        pred.Parameters["Temple"] = card.temple.ToString();
        pred.Parameters["Tribes"] = card.tribes.Select(x => x.ToString());
        pred.Parameters["Traits"] = card.traits.Select(x => x.ToString());
        // 3. Abilities, Special Abilities
        pred.Parameters["Abilities"] = card.Abilities.Select(x => x.ToString());
        pred.Parameters["SpecialAbilities"] = card.SpecialAbilities.Select(x => x.ToString());

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

    public static int? SafelyCastInt(Expression? expression)
    {
        object? result;
        try
        {
            result = expression?.Evaluate();
        }
        catch (Exception)
        {
            Plugin.LogError($"Invalid expression: {expression?.Error ?? "(null)"}");
            return null; // Default to 'true'.
        }

        if (result == null || result is not int)
        {
            Plugin.LogError($"Invalid expression: Expression doesn't evaluate to integer!");
            return null;
        }

        return (int) result;
    }
}
