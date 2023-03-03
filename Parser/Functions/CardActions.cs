using System;
using System.Linq;
using System.Collections.Generic;
using MiscellaneousJSON.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;

namespace MiscellaneousJSON.Parser.Functions;
using CardAction = PlayableCardAction<string>; 

public static class CardActions
{
    public static string[] Names =
    {
        // TODO
    };

    public static Dictionary<string, CardAction> Functions = new()
    {
        // TODO
    };

    public static void AddAbility(ref PlayableCard card, string abilityParam)
        => card.AddTemporaryMod(new (CardData.GetAbility(abilityParam))); 
    
    public static void AttackUp(ref PlayableCard card, string attackParam)
    {
        int? attackModifier = int.TryParse(attackParam, out int x) ? x : null;
        if (attackModifier == null)
        {
            Plugin.LogError($"Invalid attack param: {attackParam ?? "(null)"}");
            return;
        } 

        CardModificationInfo mod = new();
        mod.attackAdjustment += attackModifier ?? 0;
        if (mod.attackAdjustment != 0) card.AddTemporaryMod(mod);
    }

    public static void HealthUp(ref PlayableCard card, string healthParam)
    {
        int? healthModifier = int.TryParse(healthParam, out int x) ? x : null;
        if (healthModifier == null)
        {
            Plugin.LogError($"Invalid health param: {healthParam ?? "(null)"}");
            return;
        }

        CardModificationInfo mod = new();
        mod.healthAdjustment += healthModifier ?? 0;
        if (mod.healthAdjustment != 0) card.AddTemporaryMod(mod);
    }
}
