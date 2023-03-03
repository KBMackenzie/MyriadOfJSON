using System;
using System.Linq;
using System.Collections.Generic;
using MiscellaneousJSON.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using MiscellaneousJSON.Parser.Names;

namespace MiscellaneousJSON.Parser.Functions;
using CardAction = PlayableCardAction<string>; 

public static class CardActions
{
    public static string[] Names =
    {
        FunctionNames.AddAbility,
        FunctionNames.AttackMod,
        FunctionNames.HealthMod
        // TODO
    };

    public static Dictionary<string, CardAction> Functions = new()
    {
        { FunctionNames.AddAbility, AddAbility },
        { FunctionNames.AttackMod, AttackMod },
        { FunctionNames.HealthMod, HealthMod } 
        // TODO
    };

    public static void AddAbility(ref PlayableCard card, string abilityParam)
        => card.AddTemporaryMod(new (CardData.GetAbility(abilityParam))); 
    
    public static void AttackMod(ref PlayableCard card, string attackParam)
    {
        if (!int.TryParse(attackParam, out int attackModifier))
        {
            Plugin.LogError($"Invalid attack param: {attackParam ?? "(null)"}");
            return;
        } 

        CardModificationInfo mod = new();
        mod.attackAdjustment += attackModifier;
        card.AddTemporaryMod(mod);
    }

    public static void HealthMod(ref PlayableCard card, string healthParam)
    {
        if (!int.TryParse(healthParam, out int healthModifier))
        {
            Plugin.LogError($"Invalid health param: {healthParam ?? "(null)"}");
            return;
        }

        CardModificationInfo mod = new();
        mod.healthAdjustment += healthModifier;
        card.AddTemporaryMod(mod);
    }
}
