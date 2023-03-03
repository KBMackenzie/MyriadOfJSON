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

            if ((data.onlyAllowTraderChoice ?? true)
                && !x.HasCardMetaCategory(CardMetaCategory.TraderOffer)
            ) return false;

            if (!(data.allowGiantCards ?? false) && (
                x.HasTrait(Trait.Giant)
                || x.HasSpecialAbility(SpecialTriggeredAbility.GiantCard)
            )) return false;

            if (!(data.allowRareCards ?? false)
                && x.HasCardMetaCategory(CardMetaCategory.Rare)
            ) return false;
            
            Expression? pred = ExpressionHandler.CardPredicate(exp, x);
            if (pred == null) return true; // Default to true!

            return ExpressionHandler.SafeEvaluation(pred); 
        });
}
