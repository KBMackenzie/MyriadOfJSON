using System.Linq;
using System.Collections.Generic;
using NCalc;
using DiskCardGame;
using InscryptionAPI.Card;
using MiscellaneousJSON.Parser;

namespace MiscellaneousJSON.Pelts;

internal static class PeltParser
{
    /* A few notes:
     * 1. This is costly. It's fine, since it's only gonna happen once.
     * 2. Expressions with errors in them will result in a card pool with all cards by default. */

    internal static List<CardInfo> ParseCardChoices(PeltData data)
        => CardManager.AllCardsCopy.FindAll(x =>
        {
            // A lot to unpack here, uhm, here we go. :'3
            string exp = data.condition ?? "true";

            if (!(data.allowGiantCards ?? false) && (
                    x.HasTrait(Trait.Giant)
                    || x.HasSpecialAbility(SpecialTriggeredAbility.GiantCard)
            )) return false;

            if (!(data.allowRareCards ?? false)
                && x.HasCardMetaCategory(CardMetaCategory.Rare)
            ) return false;
            
            /*
            // Expression is a predicate to filter the cards with.
            Expression pred = new Expression(exp);
            // Parameters!
            // 1. Costs
            pred.Parameters["BloodCost"] = x.BloodCost;
            pred.Parameters["BoneCost"] = x.BonesCost;
            pred.Parameters["EnergyCost"] = x.EnergyCost;
            pred.Parameters["GemsCost"] = x.GemsCost.Select(x => x.ToString()); // IEnumerable<string> !! 
            // 2. Temple, Tribes, Traits
            pred.Parameters["Temple"] = x.temple.ToString();
            pred.Parameters["Tribes"] = x.tribes.Select(x => x.ToString());
            pred.Parameters["Traits"] = x.traits.Select(x => x.ToString());
            // 3. Abilities, Special Abilities
            pred.Parameters["Abilities"] = x.Abilities.Select(x => x.ToString());
            pred.Parameters["SpecialAbilities"] = x.SpecialAbilities.Select(x => x.ToString());
            */

            Expression? pred = ExpressionHandler.CardPredicate(exp, x);
            if (pred == null) return true; // Default to true!

            object? result = null;

            try
            {
                result = pred.Evaluate();
            }
            catch(EvaluationException e)
            {
                Plugin.LogError($"Error caught in expression: {exp ?? "(null)"}");
                Plugin.LogError(e.Message);
                return true; // Defaults to accepting any and all cards in case of an error.
            }

            return result is bool b && b;
        });
}
